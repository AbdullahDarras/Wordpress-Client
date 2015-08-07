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


using System;
using UIKit;
using Foundation;

namespace HttpClient
{
			
	class StringDataSource : UITableViewDataSource {
		static NSString kDefaultCell_ID = new NSString ("f");
		string [] source;
		UITableView tableView;
		
		public StringDataSource (UITableView tableView, string [] source)
		{
			this.source = source;
			this.tableView = tableView;
		}
		
		public string [] Source {
			get {
				return source;
			}
			
			set {
				source = value;
				tableView.ReloadData ();
			}
		}
		
		public override nint NumberOfSections (UITableView tableView)
		{
			return 1;
		}
		
		public override nint RowsInSection (UITableView tableView, nint section)
		{
			return source.Length;
		}
		
		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell (kDefaultCell_ID);
			if (cell == null){
				cell = new UITableViewCell (UITableViewCellStyle.Default, kDefaultCell_ID){
					SelectionStyle = UITableViewCellSelectionStyle.Blue,
				};
				cell.IndentationWidth = 30f;
			}
					
			cell.TextLabel.Text = source [indexPath.Row];
			return cell;
		}			
	}
	
	public class StringDelegate : UITableViewDelegate {
		internal int selected;
		
		public StringDelegate ()
		{
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			tableView.CellAt (NSIndexPath.FromRowSection (selected, 0)).Accessory = UITableViewCellAccessory.None;
			selected = indexPath.Row;
			tableView.CellAt (indexPath).Accessory = UITableViewCellAccessory.Checkmark;
			tableView.DeselectRow (indexPath, true);
		}

	}

	public static class TableViewSelector
	{
		public static void Configure (UITableView tableView, string [] choices)
		{
			tableView.DataSource = new StringDataSource (tableView, choices);
			tableView.Delegate = new StringDelegate ();
			tableView.SelectRow (NSIndexPath.FromRowSection (0, 0), true, UITableViewScrollPosition.None);
		}
		
		public static int SelectedRow (this UITableView tableView)
		{
			var d = tableView.Delegate as StringDelegate;
			if (d == null)
				return 0;
			return d.selected;
		}
	}
	
	
}
