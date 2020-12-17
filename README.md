# Prism.Generators.PageConstants

[![Build Status](https://dev.azure.com/chowarth23/Prism.Generators.PageConstants/_apis/build/status/Release-Pipeline?branchName=main)](https://dev.azure.com/chowarth23/Prism.Generators.PageConstants/_build/latest?definitionId=11&branchName=main)

Are you tired of relying on hard-coded strings or manually maintaining a set of constants for navigation when using Prism? If so, then this is the library for you! Our forever friends C# & Roslyn have got some helpful features that we can take advangtage of to make your life easier!

`Prism.Generators.PageConstants` will automagically generate a class that contains all the constants you'll need for registering pages for navigation and for use in navigation itself.

## How to use:

- Add the `Prism.Generators.PageConstants` NuGet package to the project where you want to store the generated constants.
- Add the `PageConstant` attribute to your Xamarin.Forms page(s), optionally providing a `Name`.
- Build your project.

Once your project has built, under the `Prism.Generated` namespace, will exist the `PageConstants` class. Using this update your project as below:
- If you have provided a custom `Name` for any of the atributes, you'll need to also use the generated constant when registering the page for navigation with Prism e.g. `containerRegistry.RegisterForNavigation<MyPage, MyPageViewModel>(PageConstants.MyPage);`.
- Update any hard-coded strings used when calling `NavigateAsync`.
  - e.g. `NavigationService.NavigateAsync("MyPage");` becomes `NavigationService.NavigateAsync(PageConstants.MyPage);`
- And that's it, enjoy!