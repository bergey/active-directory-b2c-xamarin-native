module TaskClient.App

open TaskClient.Config
open TaskClient.WelcomePage

open Microsoft.Identity.Client
open System.Collections.Generic
open System.Collections.ObjectModel
open Xamarin.Forms

type App() =
    inherit Application()

    let mutable PCApplication : PublicClientApplication = PublicClientApplication(Authority, ClientID)

    do base.MainPage <- NavigationPage(WelcomePage(PCApplication))

    member val Tasks : ObservableCollection<string> = ObservableCollection() with get, set