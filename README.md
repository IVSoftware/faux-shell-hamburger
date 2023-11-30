﻿# Faux Shell Hamburger Flyout

I like making controls from scratch that mimic the default functionality, it's good for my learning, so here's a _basic_ starter for what you're trying to do. As I understand it, you would like a hamburger-flyout style menu without the shell. So, from the default MAUI project, first delete the `AppShell` files and make `MainPage` (instead of `AppShell`) your main page.

```
public App()
{
    InitializeComponent();
    MainPage = new MainPage();
}
```

Relative to the default MAUI xaml for MainPage, you need a hamburger button, a translucent gray overlay, and a list of your menu items. To "show the flyout" make the overlay visible and ease the menu stack in using `TranslateTo` which is a main ingredient of animation. Reverse the process to hide it. This panel shows with and without the overlay and menu visible.

[![with and without flyout][1]][1]

```
public partial class MainPage : ContentPage
{
    private async void ShowFlyout(object sender, EventArgs e)
    {
        Overlay.IsVisible = true;
        await Flyout.TranslateTo(0, 0, 250, Easing.SinInOut);
    }
    private async void HideFlyout(object sender, EventArgs e)
    {
        await Flyout.TranslateTo(-250, 0, 250, Easing.SinInOut);
        Overlay.IsVisible = false;
    }
    private void OnMenuItemTap(object sender, TappedEventArgs e)
    {
        if(e.Parameter is string text)
        {
            switch (text) 
            {
                case "Flights":
                    break;
                case "Beach Activities":
                    break;
                case "Mail":
                    break;
                case "Photos":
                    break;
                case "Awards":
                    break;
            }
        }
        HideFlyout(sender, e);
    }
    private void OnOverlayTap(object sender, TappedEventArgs e) =>
        HideFlyout(sender, e);
}
```

Here's an example layout using random Unicode glyphs for button text:  ✈, 🌞, ✉, 📸,🏆 where the original MAUI xml now lives in row 1 of a grid, with `Overlay` over it and `Flyout` over that. The '☰' menu is in grid row 0. 

```
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="faux_shell_hamburger.MainPage">
    <ContentPage.Resources>
        <ResourceDictionary>
            <Style x:Key="IconLabelStyle" TargetType="Label">
                <Setter Property="FontSize" Value="Medium" />
                <Setter Property="WidthRequest" Value="50" />
                <Setter Property="VerticalTextAlignment" Value="Center" />
                <Setter Property="HorizontalTextAlignment" Value="Center" />
                <Setter Property="TextColor" Value="{StaticResource Secondary}" />
                <Setter Property="BackgroundColor" Value="{StaticResource Primary}" />
            </Style>
            <Style x:Key="LabelStyle" TargetType="Label">
                <Setter Property="VerticalTextAlignment" Value="Center" />
                <Setter Property="Padding" Value="5,0" />
                <Setter Property="FontSize" Value="Medium" />
            </Style>
        </ResourceDictionary>
    </ContentPage.Resources>
    <ScrollView>
        <Grid
            RowDefinitions="50,*">
            <Button
                Text="☰"
                FontSize="Medium"
                CornerRadius="0"
                HorizontalOptions="Start"
                WidthRequest="50"
                TextColor="{StaticResource Primary}"
                BackgroundColor="{StaticResource Secondary}"
                Clicked="ShowFlyout"/>
        <VerticalStackLayout
            Grid.Row="1"
            Padding="30,0"
            Spacing="25">
            <Image
                Source="dotnet_bot.png"
                HeightRequest="185"
                Aspect="AspectFit"
                SemanticProperties.Description="dot net bot in a race car number eight" />
            <Label
                Text="Hello, World!"
                Style="{StaticResource Headline}"
                SemanticProperties.HeadingLevel="Level1" />
            <Label
                Text="Welcome to &#10;.NET Multi-platform App UI"
                Style="{StaticResource SubHeadline}"
                SemanticProperties.HeadingLevel="Level2"
                SemanticProperties.Description="Welcome to dot net Multi platform App U I" />
            <Button
                x:Name="CounterBtn"
                Text="Click me" 
                SemanticProperties.Hint="Counts the number of times you click"
                Clicked="OnCounterClicked"
                HorizontalOptions="Fill" />
            </VerticalStackLayout>
            <Frame
                x:Name="Overlay"
                IsVisible="false"
                Grid.Row="1"
                BackgroundColor="DarkGray"
                Opacity="0.8">
                <Frame.GestureRecognizers>
                    <TapGestureRecognizer Tapped="OnOverlayTap"/>
                </Frame.GestureRecognizers>
            </Frame>
            <VerticalStackLayout
                x:Name="Flyout"
                WidthRequest="250"
                HorizontalOptions="Start"
                Spacing="2"
                Grid.Row="1"
                BackgroundColor="WhiteSmoke"
                TranslationX="-250">
                <HorizontalStackLayout HorizontalOptions="Start" HeightRequest="50" >
                    <Label Text="✈" Style="{StaticResource IconLabelStyle}"/>
                    <Label Text="Flights" Style="{StaticResource LabelStyle}"/>
                    <HorizontalStackLayout.GestureRecognizers>
                        <TapGestureRecognizer Tapped="OnMenuItemTap" CommandParameter="Flights"/>
                    </HorizontalStackLayout.GestureRecognizers>
                </HorizontalStackLayout>
                <HorizontalStackLayout HorizontalOptions="Start" HeightRequest="50" >
                    <Label Text="🌞" Style="{StaticResource IconLabelStyle}"/>
                    <Label Text="Beach Activities" Style="{StaticResource LabelStyle}"/>
                    <HorizontalStackLayout.GestureRecognizers>
                        <TapGestureRecognizer Tapped="OnMenuItemTap" CommandParameter="Beach Activities"/>
                    </HorizontalStackLayout.GestureRecognizers>
                </HorizontalStackLayout>
                <HorizontalStackLayout HorizontalOptions="Start" HeightRequest="50">
                    <Label Text="✉" Style="{StaticResource IconLabelStyle}"/>
                    <Label Text="Mail" Style="{StaticResource LabelStyle}"/>
                    <HorizontalStackLayout.GestureRecognizers>
                        <TapGestureRecognizer Tapped="OnMenuItemTap" CommandParameter="Mail"/>
                    </HorizontalStackLayout.GestureRecognizers>
                </HorizontalStackLayout>
                <HorizontalStackLayout HorizontalOptions="Start" HeightRequest="50">
                    <Label Text="📸" Style="{StaticResource IconLabelStyle}"/>
                    <Label Text="Photos" Style="{StaticResource LabelStyle}"/>
                    <HorizontalStackLayout.GestureRecognizers>
                        <TapGestureRecognizer Tapped="OnMenuItemTap" CommandParameter="Photos"/>
                    </HorizontalStackLayout.GestureRecognizers>
                </HorizontalStackLayout>
                <HorizontalStackLayout HorizontalOptions="Start" HeightRequest="50">
                    <Label Text="🏆" Style="{StaticResource IconLabelStyle}"/>
                    <Label Text="Awards" Style="{StaticResource LabelStyle}"/>
                    <HorizontalStackLayout.GestureRecognizers>
                        <TapGestureRecognizer Tapped="OnMenuItemTap" CommandParameter="Awards"/>
                    </HorizontalStackLayout.GestureRecognizers>
                </HorizontalStackLayout>
            </VerticalStackLayout>
        </Grid>
    </ScrollView>
</ContentPage>
```

Now you're on your own for designing your app's navigation without help from `AppShell` but at least now you know how to make a basic "drawer" action when you need it.



  [1]: https://i.stack.imgur.com/zDbNJ.png