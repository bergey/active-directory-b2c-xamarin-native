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
    let OnSignIn _ =
        try
            let ar =
                pca.AcquireTokenAsync(Scopes, "", UiOptions.SelectAccount, "", null, Authority, Policy)
                |> Async.AwaitTask
                |> Async.RunSynchronously
            displayAlert "B2C Response" (sprintf "Token: %s" ar.Token) "Dismiss"
            pushPage(TasksPage(pca)) |> ignore
        with
        | ee -> displayAlert "Exception" ee.Message "Dismiss"

    do  signIn.Clicked.Add(fun _ -> invokeOnMainThread OnSignIn)
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