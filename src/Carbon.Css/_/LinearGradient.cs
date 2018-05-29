﻿using System;
using System.Collections.Generic;

namespace Carbon.Css
{
    internal readonly struct LinearGradient
    {
        private readonly CssValueList args;

        public LinearGradient(CssValueList args)
        {
            this.args = args ?? throw new ArgumentNullException(nameof(args));
        }

        // TODO (Direction)
        // TODO (Stops)

        // Standard
        public override string ToString() => $"linear-gradient({args})";

        public IEnumerable<CssFunction> ExpandFor(BrowserInfo[] browsers)
        {
            foreach (BrowserInfo browser in browsers)
            {
                var values = new List<CssValue>();

                var i = 0;
                foreach (var arg in args)
                {
                    // The legacy syntax didn't contain to on the direction
                    if (i == 0 && arg.ToString().StartsWith("to "))
                    {
                        values.Add(CssValue.Parse(arg.ToString().Replace("to ", "")));
                    }
                    else
                    {
                        values.Add(arg);
                    }

                    i++;
                }

                var name = browser.Prefix.Text + "linear-gradient";

                yield return new CssFunction(name, new CssValueList(values, ValueSeperator.Comma));
            }
        }
    }

    public enum Direction
    {
        Top,
        Bottom,
        Left,
        Right
    }

    // background: linear-gradient(angle, color-stop1, color-stop2);
    // [ <angle> | to [top | bottom] || [left | right] ],]? <color-stop>[, <color-stop>]+);
}

/*
background: -webkit-linear-gradient(left,rgba(255,0,0,0),rgba(255,0,0,1)); // Safari 5.1-6
background: -o-linear-gradient(right,rgba(255,0,0,0),rgba(255,0,0,1));	   // Opera 11.1-12
background: -moz-linear-gradient(right,rgba(255,0,0,0),rgba(255,0,0,1));   // Fx 3.6-15
*/

/*
background: -webkit-linear-gradient(red, blue); // For Safari 5.1 to 6.0
background: -o-linear-gradient(red, blue);		// For Opera 11.1 to 12.0
background: -moz-linear-gradient(red, blue);	// For Firefox 3.6 to 15
background: linear-gradient(red, blue);			// Standard syntax
*/

/*
-moz-linear-gradient(right,  rgba(237,102,136,1) 0%, rgba(250,179,42,1) 100%);
-webkit-gradient(linear, right top, right top, color-stop(0%,rgba(237,102,136,1)), color-stop(100%,rgba(250,179,42,1)));

-webkit-linear-gradient(right, rgba(237,102,136,1) 0%,rgba(250,179,42,1) 100%); 
-ms-linear-gradient(right, rgba(237,102,136,1) 0%,rgba(250,179,42,1) 100%); 
linear-gradient(to right, rgba(237,102,136,1) 0%,rgba(250,179,42,1) 100%);
*/
