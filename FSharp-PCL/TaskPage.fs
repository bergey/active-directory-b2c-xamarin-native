module TaskClient.TasksPage

open TaskClient.Config

open Microsoft.Identity.Client
open System.Collections.Generic
open System.Net.Http
open Xamarin.Forms

type TasksPage(pca : PublicClientApplication) =
    inherit ContentPage()

    member val platformParameters = null with get, set

    override this.OnAppearing() =
        pca.PlatformParameters <- this.platformParameters
        do base.OnAppearing()
        try
            async {
                let! ar = pca.AcquireTokenSilentAsync(Scopes, "", Authority, Policy, false) |> Async.AwaitTask
                displayAlert "B2C Response" (sprintf "Token: %s" ar.Token) "Dismiss"
            } |> Async.RunSynchronously
        with
        | ee ->
            Application.Current.MainPage.DisplayAlert("An error has occurred in TasksPage auth", "Exception message: " + ee.Message, "Dismiss")
                |> ignore