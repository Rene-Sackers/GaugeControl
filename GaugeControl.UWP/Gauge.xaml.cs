using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using GaugeControl.UWP.Models;

namespace GaugeControl.UWP
{
    /// <summary>
    /// Interaction logic for Gauge.xaml
    /// </summary>
    public partial class Gauge
    {
        public static readonly DependencyProperty MinAngleProperty = DependencyProperty.Register(
            nameof(MinAngle), typeof(int), typeof(Gauge), new PropertyMetadata(0, TickRelatedPropertyChanged));

        public int MinAngle
        {
            get { return (int) GetValue(MinAngleProperty); }
            set { SetValue(MinAngleProperty, value); }
        }

        public static readonly DependencyProperty MaxAngleProperty = DependencyProperty.Register(
            nameof(MaxAngle), typeof(int), typeof(Gauge), new PropertyMetadata(0, TickRelatedPropertyChanged));

        public int MaxAngle
        {
            get { return (int) GetValue(MaxAngleProperty); }
            set { SetValue(MaxAngleProperty, value); }
        }

        private int TotalMaxAngle => MinAngle * -1 + 180 + MaxAngle;

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value", typeof(double), typeof(Gauge), new PropertyMetadata(0.0, AngleRelatedPropertyChanged));

        public double Value
        {
            get { return (double) GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueNeedleAngleProperty = DependencyProperty.Register(
            "ValueNeedleAngle", typeof(double), typeof(Gauge), new PropertyMetadata(default(double)));

        public double ValueNeedleAngle
        {
            get { return (double) GetValue(ValueNeedleAngleProperty); }
            set { SetValue(ValueNeedleAngleProperty, value); }
        }

        public static readonly DependencyProperty MaxValueProperty = DependencyProperty.Register(
            "MaxValue", typeof(double), typeof(Gauge), new PropertyMetadata(100.0, TickRelatedPropertyChanged));

        public double MaxValue
        {
            get { return (double) GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        public static readonly DependencyProperty MinValueProperty = DependencyProperty.Register(
            "MinValue", typeof(double), typeof(Gauge), new PropertyMetadata(0.0, TickRelatedPropertyChanged));
        public double MinValue
        {
            get { return (double) GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }

        public static readonly DependencyProperty TickCountProperty = DependencyProperty.Register(
            "TickCount", typeof(int), typeof(Gauge), new PropertyMetadata(11, TickRelatedPropertyChanged));

        public int TickCount
        {
            get { return (int) GetValue(TickCountProperty); }
            set { SetValue(TickCountProperty, value); }
        }

        public static readonly DependencyProperty InterTicksEnabledProperty = DependencyProperty.Register(
            "InterTicksEnabled", typeof (bool), typeof (Gauge), new PropertyMetadata(true, TickRelatedPropertyChanged));

        public bool InterTicksEnabled
        {
            get { return (bool) GetValue(InterTicksEnabledProperty); }
            set { SetValue(InterTicksEnabledProperty, value); }
        }

        public ObservableCollection<Tick> Ticks { get; set; } = new ObservableCollection<Tick>();

        public Gauge()
        {
            InitializeComponent();

            UpdateNeedleAngle();
            UpdateTicks();
        }

        private static void AngleRelatedPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var gauge = (Gauge) dependencyObject;
            gauge.UpdateNeedleAngle();
        }

        private static void TickRelatedPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var gauge = (Gauge)dependencyObject;
            gauge.UpdateTicks();
        }

        public void UpdateNeedleAngle()
        {
            // Sanity checks
            if (MinAngle < -90 || MinAngle > 270)
                throw new InvalidOperationException($"{nameof(MinAngle)} out of range. Minimum: -90, maximum: 270");

            if (MinAngle > MaxAngle + 180)
                throw new InvalidOperationException($"{nameof(MinAngle)} exceeds {nameof(MaxAngle)}");

            if (MaxAngle + 180 < MinAngle)
                throw new InvalidOperationException($"{nameof(MaxAngle)} exceeds {nameof(MinAngle)}");

            if (MaxAngle < -270 || MaxAngle > 90)
                throw new InvalidOperationException($"{nameof(MaxAngle)} out of range. Minimum: -270, maximum: 90");

            if (Value < MinValue)
                throw new InvalidOperationException($"{nameof(Value)} is lower than {nameof(MinValue)}.");

            if (Value > MaxValue)
                throw new InvalidOperationException($"{nameof(Value)} exceeds {nameof(MaxValue)}.");
            
            if (MaxValue <= 0 || MinValue > MaxValue)
            {
                ValueNeedleAngle = MinAngle;
                return;
            }
            
            var anglePerUnit = TotalMaxAngle / MaxValue;

            ValueNeedleAngle = Value * anglePerUnit + MinAngle;
        }

        private void UpdateTicks()
        {
            var tickIntervalAngle = (double)TotalMaxAngle/(TickCount - 1);
            var tickValueInverval = MaxValue/(TickCount - 1);

            Ticks.Clear();

            var isInterTick = true;
            for (var i = 0; i < TickCount; i++)
            {
                Ticks.Add(new Tick
                {
                    Angle = tickIntervalAngle * i + MinAngle,
                    IsInterTick = InterTicksEnabled && (isInterTick = !isInterTick),
                    Value = (int)Math.Round(tickValueInverval * i)
                });
            }

            UpdateNeedleAngle();
        }
    }
}
