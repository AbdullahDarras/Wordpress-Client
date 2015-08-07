// The MIT License (MIT)
//
// Copyright (c) 2015 FPT Software
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using Android.Content;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.Droid.Platform;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.DownloadCache;
using FSoft.WordApp.Core;
using FSoft.WordApp.Droid;
using Cirrious.MvvmCross.Binding.Bindings.Target.Construction;
using Android.Widget;
using FSoft.WordApp.Droid.Binding;

namespace FSoft.WordApp.Droid
{
	public class Setup : MvxAndroidSetup
	{
		public Setup(Context applicationContext) : base(applicationContext)
		{
		}

		protected override IMvxApplication CreateApp()
		{
			return new Core.App();
		}

		protected override IMvxTrace CreateDebugTrace()
		{
			return new DebugTrace();
		}

		protected override void InitializeLastChance()
		{
			base.InitializeLastChance();
			Cirrious.MvvmCross.Plugins.File.PluginLoader.Instance.EnsureLoaded();
			Cirrious.MvvmCross.Plugins.DownloadCache.PluginLoader.Instance.EnsureLoaded();
			Cirrious.MvvmCross.Plugins.Visibility.PluginLoader.Instance.EnsureLoaded ();
			Cirrious.MvvmCross.Plugins.WebBrowser.PluginLoader.Instance.EnsureLoaded ();

			EShyMedia.MvvmCross.Plugins.DeviceInfo.PluginLoader.Instance.EnsureLoaded ();
			//Beezy.MvvmCross.Plugins.SecureStorage.PluginLoader.Instance.EnsureLoaded ();

			Mvx.RegisterSingleton<IMvxHttpFileDownloader>(() => new MvxFastHttpFileDownloader());

			Mvx.RegisterSingleton<IBuildDetails> (() => new AndroidBuildDetails ());
			Mvx.RegisterSingleton<Beezy.MvvmCross.Plugins.SecureStorage.IMvxProtectedData> (() => new Beezy.MvvmCross.Plugins.SecureStorage.Droid.MvxAndroidProtectedData ());
		}

		protected override void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
		{
			base.FillTargetFactories(registry);
			registry.RegisterFactory(new MvxCustomBindingFactory<RelativeLayout>("ShapeBackground", (view) => new UserAvatarShapeBackgroundBinding(view)));
		}
	}
}
