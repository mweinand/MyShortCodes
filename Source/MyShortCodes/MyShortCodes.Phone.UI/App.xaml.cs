using System;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MyShortCodes.Phone.CommandHandlers;
using MyShortCodes.Phone.Commands;
using MyShortCodes.Phone.Infrastructure.Messaging;
using MyShortCodes.Phone.State;
using MyShortCodes.Phone.Infrastructure.Container;
using MyShortCodes.Phone.Storage;
using MyShortCodes.Phone.UI.Navigation;
using MyShortCodes.Phone.Navigation;
using MyShortCodes.Phone.ViewModels;
using MyShortCodes.Phone.Validation;
using MyShortCodes.Phone.Domain;
using MyShortCodes.Phone.Services;
using MyShortCodes.Phone.Infrastructure.Threads;
using MyShortCodes.Phone.UI.Infrastructure.Threads;

namespace MyShortCodes.Phone.UI
{
    public partial class App : Application
    {
        private readonly Action<IContainer> _initializationAction;

        /// <summary>
        /// Provides easy access to the root frame of the Phone Application.
        /// </summary>
        /// <returns>The root frame of the Phone Application.</returns>
        public PhoneApplicationFrame RootFrame { get; private set; }
        
        /// <summary>
        /// Constructor for the Application object.
        /// </summary>
        public App()
        {
            // Global handler for uncaught exceptions. 
            UnhandledException += Application_UnhandledException;

            // Show graphics profiling information while debugging.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // Display the current frame rate counters
                //Application.Current.Host.Settings.EnableFrameRateCounter = true;

                // Show the areas of the app that are being redrawn in each frame.
                //Application.Current.Host.Settings.EnableRedrawRegions = true;

                // Enable non-production analysis visualization mode, 
                // which shows areas of a page that are being GPU accelerated with a colored overlay.
                //Application.Current.Host.Settings.EnableCacheVisualization = true;
            }

            // Standard Silverlight initialization
            InitializeComponent();

            // Phone-specific initialization
            InitializePhoneApplication();

            // setup container
            var state = new ApplicationState();
            var commandBus = new CommandBus();

            var mainViewModel = new MainViewModel();
            var addPageViewModel = new AddPageViewModel();

            _initializationAction = (IContainer x) =>
            {
                x.Register<ISettingsManager, SettingsManager>();
                x.Register<IStorageManager, StorageManager>();
                x.Register<IUIThreadInvoker, UIThreadInvoker>();
                x.Register<ICommandBus>(commandBus);
                x.Register<IApplicationState>(state);

                x.Register<INavigationServiceWrapper, NavigationServiceWrapper>();
                x.Register<IDialogService, DialogService>();
                x.Register<ITrialService, TrialService>();

                x.Register<ICommandHandler<ApplicationLoadedCommand>, ApplicationLoadedCommandHandler>();
                x.Register<ICommandHandler<MainPageLoadedCommand>, MainPageLoadedCommandHandler>();
                x.Register<ICommandHandler<AddNewShortCodeCommand>, AddNewShortCodeCommandHandler>();
                x.Register<ICommandHandler<EditShortCodeCommand>, EditShortCodeCommandHandler>();
                x.Register<ICommandHandler<SaveShortCodeCommand>, SaveShortCodeCommandHandler>();
                x.Register<ICommandHandler<DeleteShortCodeCommand>, DeleteShortCodeCommandHandler>();
                x.Register<ICommandHandler<SendSmsCommand>, SendSmsCommandHandler>();
                x.Register<ICommandHandler<DataLoadedMessage>, DataLoadedCommandHandler>();

                x.Register<IValidator<ShortCode>, ShortCodeValidator>();

                x.Register<IMainViewModel>(mainViewModel);
                x.Register<IAddPageViewModel>(addPageViewModel);
            };
            MicroMap.Initialize(_initializationAction);

            commandBus.RegisterHandler(MicroMap.GetInstance<ICommandHandler<ApplicationLoadedCommand>>());
            commandBus.RegisterHandler(MicroMap.GetInstance<ICommandHandler<MainPageLoadedCommand>>());
            commandBus.RegisterHandler(MicroMap.GetInstance<ICommandHandler<AddNewShortCodeCommand>>());
            commandBus.RegisterHandler(MicroMap.GetInstance<ICommandHandler<EditShortCodeCommand>>());
            commandBus.RegisterHandler(MicroMap.GetInstance<ICommandHandler<SaveShortCodeCommand>>());
            commandBus.RegisterHandler(MicroMap.GetInstance<ICommandHandler<DeleteShortCodeCommand>>());
            commandBus.RegisterHandler(MicroMap.GetInstance<ICommandHandler<SendSmsCommand>>());
            commandBus.RegisterHandler(MicroMap.GetInstance<ICommandHandler<DataLoadedMessage>>());

        }

        // Code to execute when the application is launching (eg, from Start)
        // This code will not execute when the application is reactivated
        private void Application_Launching(object sender, LaunchingEventArgs e)
        {
            var commandBus = MicroMap.GetInstance<ICommandBus>();
            commandBus.PublishCommand(new ApplicationLoadedCommand());
        }

        // Code to execute when the application is activated (brought to foreground)
        // This code will not execute when the application is first launched
        private void Application_Activated(object sender, ActivatedEventArgs e)
        {
            var commandBus = MicroMap.GetInstance<ICommandBus>();
            commandBus.PublishCommand(new ApplicationLoadedCommand());
        }

        // Code to execute when the application is deactivated (sent to background)
        // This code will not execute when the application is closing
        private void Application_Deactivated(object sender, DeactivatedEventArgs e)
        {
        }

        // Code to execute when the application is closing (eg, user hit Back)
        // This code will not execute when the application is deactivated
        private void Application_Closing(object sender, ClosingEventArgs e)
        {
            // Ensure that required application state is persisted here.
        }

        // Code to execute if a navigation fails
        private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // A navigation has failed; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        // Code to execute on Unhandled Exceptions
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        #region Phone application initialization

        // Avoid double-initialization
        private bool _phoneApplicationInitialized = false;

        // Do not add any additional code to this method
        private void InitializePhoneApplication()
        {
            if (_phoneApplicationInitialized)
                return;

            // Create the frame but don't set it as RootVisual yet; this allows the splash
            // screen to remain active until the application is ready to render.
            RootFrame = new PhoneApplicationFrame();
            RootFrame.Navigated += CompleteInitializePhoneApplication;

            // Handle navigation failures
            RootFrame.NavigationFailed += RootFrame_NavigationFailed;

            // Ensure we don't initialize again
            _phoneApplicationInitialized = true;
        }

        // Do not add any additional code to this method
        private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
        {
            // Set the root visual to allow the application to render
            if (RootVisual != RootFrame)
                RootVisual = RootFrame;

            // Remove this handler since it is no longer needed
            RootFrame.Navigated -= CompleteInitializePhoneApplication;
        }

        #endregion
    }
}