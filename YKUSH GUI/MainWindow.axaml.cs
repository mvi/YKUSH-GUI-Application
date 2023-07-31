using System;
using System.Collections.Generic;
using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;
using static YKUSHGUI.CommandWrapper;

namespace YKUSHGUI;

public partial class MainWindow : Window
{
    private BoardType SelectedBoardType => (BoardType) deviceTypeComboBox.SelectedIndex;


    public MainWindow()
    {
        InitializeComponent();

        if (Design.IsDesignMode)
        {
            return;
        }

        deviceTypeComboBox.SelectedIndex = (int) BoardType.ykushxs;
        deviceTypeComboBox.SelectionChanged += DeviceTypeComboBox_SelectionChanged;
        RefreshVisibility();

        DispatcherTimer timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(0.1f)
        };
        timer.Tick += Timer_Tick;
        timer.Start();
    }

    private void Timer_Tick(object sender, EventArgs e)
    {
        List<string> boardSerials = CommandWrapper.ListAttachedBoards(SelectedBoardType);
        bool anyAttached = boardSerials.Count != 0;
        panelInteractive.IsEnabled = anyAttached;

        if (anyAttached)
        {
            if (SelectedBoardType != BoardType.ykush) // Has three ports
            {
                SetIndicator(indicator1, GetPortStatus(SelectedBoardType, 1));
                SetIndicator(indicator2, GetPortStatus(SelectedBoardType, 2));
                SetIndicator(indicator3, GetPortStatus(SelectedBoardType, 3));
            }
            else
            {
                SetIndicator(indicator1, GetPortStatus(SelectedBoardType));
                SetIndicator(indicator2, null);
                SetIndicator(indicator3, null);
            }

            statusLabel.Content = $"Found attached board";
        }
        else
        {
            statusLabel.Content = $"No attached board found";
            SetIndicator(indicator1, null);
            SetIndicator(indicator2, null);
            SetIndicator(indicator3, null);
        }
    }

    void SetIndicator(TextBlock indicator, bool? isOn)
    {
        indicator.Classes.Remove("On");
        indicator.Classes.Remove("Off");

        if (isOn.HasValue)
        {
            if (isOn.Value)
            {
                indicator.Classes.Add("On");
            }
            else
            {
                indicator.Classes.Add("Off");
            }
        }
    }

    private void DeviceTypeComboBox_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        RefreshVisibility();
    }

    private void RefreshVisibility()
    {
        if (SelectedBoardType != BoardType.ykush) // Has three ports
        {
            togglePort2Button.IsEnabled = true;
            togglePort3Button.IsEnabled = true;
        }
        else
        {
            togglePort2Button.IsEnabled = false;
            togglePort3Button.IsEnabled = false;
        }
    }

    void TogglePortStatus(int portIndex = -1)
    {
        bool isOn = GetPortStatus(SelectedBoardType, portIndex);
        ChangePortStatus(SelectedBoardType, !isOn, portIndex);
    }

    private void TurnOnButton_Click(object? sender, RoutedEventArgs e)
    {
        ChangePortStatus(SelectedBoardType, true);
    }

    private void TurnOffButton_Click(object? sender, RoutedEventArgs e)
    {
        ChangePortStatus(SelectedBoardType, false);
    }

    private void TogglePort1Button_Click(object? sender, RoutedEventArgs e)
    {
        TogglePortStatus(1);
    }

    private void TogglePort2Button_Click(object? sender, RoutedEventArgs e)
    {
        TogglePortStatus(2);
    }

    private void TogglePort3Button_Click(object? sender, RoutedEventArgs e)
    {
        TogglePortStatus(3);
    }
}