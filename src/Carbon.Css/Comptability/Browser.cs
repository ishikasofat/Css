﻿namespace Carbon.Css
{
	using System;

	public class Browser
	{
		private readonly BrowserType type;
		private readonly float version;

		public Browser(BrowserType type, float version)
		{
			this.type = type;
			this.version = version;
		}

		public string Prefix
		{
			get { return GetPrefix(type); }
		}

		public static Browser Chrome(float version)
		{
			return new Browser(BrowserType.Chrome, version);
		}

		public static Browser Firefox(float version)
		{
			return new Browser(BrowserType.Firefox, version);
		}

		public static Browser Safari(float version)
		{
			return new Browser(BrowserType.Safari, version);
		}

		public static Browser Opera(float version)
		{
			return new Browser(BrowserType.Opera, version);
		}

		public static Browser IE(float version)
		{
			return new Browser(BrowserType.IE, version);
		}

		public static readonly Browser Chrome1  = Chrome(1);
		public static readonly Browser Chrome7  = Chrome(7);
		public static readonly Browser Chrome10 = Chrome(10);
		public static readonly Browser Chrome13 = Chrome(13);
		public static readonly Browser Chrome26 = Chrome(26);

		public static readonly Browser Firefox1  = Firefox(1);
		public static readonly Browser Firefox4  = Firefox(4);
		public static readonly Browser Firefox5  = Firefox(5);
		public static readonly Browser Firefox6  = Firefox(6);
		public static readonly Browser Firefox9  = Firefox(9);
		public static readonly Browser Firefox10 = Firefox(10);
		public static readonly Browser Firefox16 = Firefox(16);
		public static readonly Browser Firefox20 = Firefox(20);
		public static readonly Browser Firefox21 = Firefox(21);

		public static readonly Browser IE6  = IE(6);
		public static readonly Browser IE7  = IE(7);
		public static readonly Browser IE8  = IE(8);
		public static readonly Browser IE9  = IE(9);
		public static readonly Browser IE10 = IE(10);
		public static readonly Browser IE11 = IE(11);

		public static readonly Browser Opera4  = Opera(3);
		public static readonly Browser Opera9  = Opera(9);
		public static readonly Browser Opera15 = Opera(15); // Based on Chromium

		public static readonly Browser Safari1 = Safari(1);
		public static readonly Browser Safari3 = Safari(3);
		public static readonly Browser Safari4 = Safari(4);
		public static readonly Browser Safari5 = Safari(5);
		public static readonly Browser Safari6 = Safari(6);

		public static string GetPrefix(BrowserType type)
		{
			switch (type)
			{
				case BrowserType.Chrome:	return "-webkit-";
				case BrowserType.Firefox:	return "-moz-";
				case BrowserType.IE:		return "-ms-";
				case BrowserType.Opera:		return "-o-";
				case BrowserType.Safari:	return "-webkit-";

				default:					throw new Exception("Unexpected browser: " + type);
			}
		}

	}

	public enum BrowserType
	{
		IE,
		Firefox,
		Safari,
		Chrome,
		Opera
	}
}