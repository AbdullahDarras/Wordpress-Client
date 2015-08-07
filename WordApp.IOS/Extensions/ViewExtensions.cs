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

﻿using UIKit;
using System;

public static class ViewExtensions
{
	/// <summary>
	/// Find the first responder in the <paramref name="view"/>'s subview hierarchy
	/// </summary>
	/// <param name="view">
	/// A <see cref="UIView"/>
	/// </param>
	/// <returns>
	/// A <see cref="UIView"/> that is the first responder or null if there is no first responder
	/// </returns>
	public static UIView FindFirstResponder(this UIView view)
	{
		if (view.IsFirstResponder)
		{
			return view;
		}
		foreach (UIView subView in view.Subviews)
		{
			var firstResponder = subView.FindFirstResponder();
			if (firstResponder != null)
				return firstResponder;
		}
		return null;
	}

	/// <summary>
	/// Find the first Superview of the specified type (or descendant of)
	/// </summary>
	/// <param name="view">
	/// A <see cref="UIView"/>
	/// </param>
	/// <param name="stopAt">
	/// A <see cref="UIView"/> that indicates where to stop looking up the superview hierarchy
	/// </param>
	/// <param name="type">
	/// A <see cref="Type"/> to look for, this should be a UIView or descendant type
	/// </param>
	/// <returns>
	/// A <see cref="UIView"/> if it is found, otherwise null
	/// </returns>
	public static UIView FindSuperviewOfType(this UIView view, UIView stopAt, Type type)
	{
		if (view.Superview != null)
		{
			if (type.IsAssignableFrom(view.Superview.GetType()))
			{
				return view.Superview;
			}

			if (view.Superview != stopAt)
				return view.Superview.FindSuperviewOfType(stopAt, type);
		}

		return null;
	}

	public static nfloat CenterX(this UIView view) {
		return (view.Frame.Left + view.Frame.Width) / 2;
	}

	public static nfloat CenterY(this UIView view) {
		return (view.Frame.Top + view.Frame.Height) / 2;
	}

	public static void SetLeft(this UIView view, nfloat l) {
		view.Frame = new CoreGraphics.CGRect (l, view.Frame.Top, view.Frame.Width, view.Frame.Height);
	}

	public static void SetRight(this UIView view, nfloat r) {
		view.SetLeft (r - view.Frame.Width);
	}

	public static void SetTop(this UIView view, nfloat t) {
		view.Frame = new CoreGraphics.CGRect (view.Frame.Left, t, view.Frame.Width, view.Frame.Height);
	}

	public static void SetBottom(this UIView view, nfloat b) {
		view.SetTop (b - view.Frame.Height);
	}

	public static void SetWidth(this UIView view, nfloat w) {
		view.Frame = new CoreGraphics.CGRect (view.Frame.Left, view.Frame.Top, w, view.Frame.Height);
	}

	public static void SetHeight(this UIView view, nfloat h) {
		view.Frame = new CoreGraphics.CGRect (view.Frame.Left, view.Frame.Top, view.Frame.Width, h);
	}

	public static void SetCenterX(this UIView view, nfloat cx){
		nfloat cy = view.Center.Y;
		view.Center = new CoreGraphics.CGPoint (cx, cy);
	}

	public static void SetCenterY(this UIView view, nfloat cy){
		nfloat cx = view.Center.X;
		view.Center = new CoreGraphics.CGPoint (cx, cy);
	}
}
