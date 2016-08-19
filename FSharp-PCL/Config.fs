module TaskClient.Config

open Xamarin.Forms

let ClientID = "90c0fe63-bcf2-44d5-8fb7-b8bbc0b29dc6";
let Scopes : string[] = [| ClientID |];

let Policy = "b2c_1_susi";
let Authority = "https://login.microsoftonline.com/fabrikamb2c.onmicrosoft.com/";
let APIbaseURL = "https://aadb2cplayground.azurewebsites.net"

let invokeOnMainThread (f : unit -> unit) : unit =
    Device.BeginInvokeOnMainThread(System.Action(f))

let pushPage page =
    Application.Current.MainPage.Navigation.PushAsync(page)

let displayAlert title body button =
    invokeOnMainThread(fun _ ->
        Application.Current.MainPage.DisplayAlert(title, body, button)
        |> ignore)