module TaskClient.WelcomePage

open TaskClient.Config
open TaskClient.TasksPage

open Microsoft.Identity.Client
open System.Collections.Generic
open Xamarin.Forms

type WelcomePage(pca : PublicClientApplication) =
    inherit ContentPage()

    // do base.InitializeComponent()

    let layout = StackLayout()
    let signIn = Button(Text = "Sign In")
    let acquireToken = async {
                let task = pca.AcquireTokenAsync(Scopes, "", UiOptions.SelectAccount, "", null, Authority, Policy)
                let! ar = task |> Async.AwaitTask
                invokeOnMainThread (fun _ ->
                    displayAlert "B2C Response" (sprintf "Token: %s" ar.Token) "Dismiss"
                    pushPage(TasksPage(pca)) |> ignore
                )
    }

    do  signIn.Clicked.Add(fun _ -> acquireToken |> Async.Start )
        layout.Children.Add(signIn)
        base.Content <- layout

    member val platformParameters = null with get, set

    override this.OnAppearing() =
        pca.PlatformParameters <- this.platformParameters
//        try
//            async {
//                let! ar = pca.AcquireTokenSilentAsync(Scopes, "", Authority, Policy, false) |> Async.AwaitTask
//                this.Navigation.PushAsync(TasksPage(pca)) |> ignore
//            } |> Async.RunSynchronously
//        with
//        | _ -> ()