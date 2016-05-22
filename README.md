# GaugeControl

Dafaq's this?
* A simple gauge control built for Windows 10 UWP apps.  
* Minimal code, maximum performance.  
* Fully databindable.
* 100% vector/XAML based rendering
* Actually works in the XAML designer preview.

## Example
![Example](http://i.imgur.com/yuQ2NER.png)

Left gauge:

```xaml
<!-- xmlns:gauge="using:GaugeControl.UWP" -->
<gauge:Gauge
  MinAngle="0"
  MaxAngle="0"
  MinValue="0"
  MaxValue="7"
  Value="2.25"
  TickCount="15"
  Grid.Row="0"
  Grid.Column="0" />
```

Right gauge:

```xaml
<!-- xmlns:gauge="using:GaugeControl.UWP" -->
<gauge:Gauge
  MinAngle="-40"
  MaxAngle="40"
  MinValue="0"
  MaxValue="220"
  Value="47"
  TickCount="23"
  Grid.Row="0"
  Grid.Column="0" />
```

## Properties

All properties support being data-bound.

Property | Type | Default | Description
--- | --- | --- | ---
MinAngle | int | 0 | The amount of degrees to adjust the start position by. See right gauge in example.
MaxAngle | int | 0 | The amount of degrees to adjust the end position by. See right gauge in example.
Value | double | 0 | The value to indicate on the gauge.
MinValue | double | 0 | The minimum value that the gauge can display.
MaxValue | double | 100 | The maximum value that the gauge can display.
TickCount | int | 11 | The amount of ticks (black lines at the edge) the gauge should display. This *includes* the `inter ticks`. See next property.
InterTicksEnabled | bool | true | Whether the gauge should draw smaller ticks inbetween numbered ticks.

## Can I use it?
I have yet to sort out which one of the licenses I need to apply. Until I find the right one, you can use this in **ANY** situation, with **NO** warranty.  
That includes commercial usage. As long as you don't claim you made it.
