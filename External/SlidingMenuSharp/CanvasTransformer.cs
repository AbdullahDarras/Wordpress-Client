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

/* 
 * Copyright 2012-2014 Jeremy Feinstein
 * Copyright 2013-2014 Tomasz Cielecki
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *     http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
*/

using Android.Graphics;
using Android.Views.Animations;

namespace SlidingMenuSharp
{
    public interface ICanvasTransformer
    {
        /**
         * Transform canvas.
         *
         * @param canvas the canvas
         * @param percentOpen the percent open
         */
        void TransformCanvas(Canvas canvas, float percentOpen);
    }

    public class ZoomTransformer : ICanvasTransformer
    {
        public void TransformCanvas(Canvas canvas, float percentOpen)
        {
            var scale = (float) (percentOpen * 0.25 + 0.75);
            canvas.Scale(scale, scale, canvas.Width / 2f, canvas.Height / 2f);
        }
    }

    public class SlideTransformer : ICanvasTransformer
    {
        private static readonly SlideInterpolator Interpolator = new SlideInterpolator();
        public class SlideInterpolator : Java.Lang.Object, IInterpolator
        {
            public float GetInterpolation(float t)
            {
                t -= 1.0f;
                return t * t * t + 1.0f;
            }
        }

        public void TransformCanvas(Canvas canvas, float percentOpen)
        {
            canvas.Translate(0, canvas.Height * (1 - Interpolator.GetInterpolation(percentOpen)));
        }
    }

    public class ScaleTransformer : ICanvasTransformer
    {
        public void TransformCanvas(Canvas canvas, float percentOpen)
        {
            canvas.Scale(percentOpen, 1, 0, 0);
        }
    }
}
