﻿namespace Carbon.Css.Tests
{
	using System.IO;

	using NUnit.Framework;
	using System;

	[TestFixture]
	public class VendorTests : FixtureBase
	{
		[Test]
		public void Nested()
		{
			var ss = StyleSheet.Parse(File.ReadAllText(GetTestFile("nested.css").FullName));

			ss.Context.SetCompatibility(Browser.Chrome1, Browser.Safari1);

			Assert.AreEqual(@"#networkLinks .block .edit:before {
  font-family: 'carbonmade';
  font-size: 12px;
  line-height: 26px;
  color: #fff;
  text-align: center;
}
#networkLinks .block .edit {
  opacity: 0;
  position: absolute;
  top: 0;
  bottom: 0;
  right: 20px;
  margin: auto 0;
  width: 26px;
  height: 26px;
  text-align: center;
  background: #3ea9f5;
  cursor: pointer;
  border-radius: 100%;
  z-index: 105;
  -webkit-transition: margin 0.1s ease-out, opacity 0.1s ease-out;
  transition: margin 0.1s ease-out, opacity 0.1s ease-out;
}
#networkLinks .block .destroy:before {
  font-family: 'carbonmade';
  font-size: 17px;
  line-height: 26px;
  color: rgba(0, 0, 0, 0.1);
  text-align: center;
}
#networkLinks .block .destroy:hover:before { color: rgba(0, 0, 0, 0.25); }
#networkLinks .block .destroy {
  display: none;
  position: absolute;
  top: 0;
  bottom: 0;
  right: 60px;
  margin: auto 0;
  width: 26px;
  height: 26px;
  cursor: pointer;
  border-radius: 100%;
  text-align: center;
  z-index: 105;
}
#networkLinks .block .input {
  background-color: #fff;
  -webkit-box-shadow: inset 0 0 0 1px #e6e6e6;
  box-shadow: inset 0 0 0 1px #e6e6e6;
  color: #222;
  height: 40px;
  line-height: 24px;
  padding: 5px 6px 5px 165px;
  margin-left: 0;
  -webkit-box-sizing: border-box;
  box-sizing: border-box;
}
#networkLinks .block .emptyGuts,
#networkLinks .block .populatedGuts,
#networkLinks .block .editGuts {
  cursor: default;
  z-index: 100;
}
#networkLinks .block .controls { padding: 5px 0 10px 210px; }", ss.ToString());


		}

		[Test]
		public void Transform()
		{
			var sheet = StyleSheet.Parse(
@"
body { 
  transform: rotate(90);
}
");

			sheet.Context.SetCompatibility(Browser.Chrome1, Browser.Safari1, Browser.Firefox1, Browser.IE9);

			Assert.AreEqual(@"body {
  -moz-transform: rotate(90);
  -ms-transform: rotate(90);
  -webkit-transform: rotate(90);
  transform: rotate(90);
}", sheet.ToString());

		}
	}


}