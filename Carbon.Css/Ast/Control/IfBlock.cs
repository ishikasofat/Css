﻿namespace Carbon.Css
{
	public class IfBlock : CssBlock
	{
		private readonly CssValue condition;

		public IfBlock(CssValue condition)
			: base(NodeKind.If)
		{
			this.condition = condition;
		}

		public CssValue Condition => condition;

		public override CssNode CloneNode()
		{ 
			var block = new IfBlock(condition);

			foreach (var child in children)
			{
				block.Add(child);
			}

			return block;
		}
	}

	/*
	public class EachBlock : CssNode
	{
		private readonly string variableName;
		private readonly CssValue list;

		public EachBlock(string variableName, CssValue list)
			: base(NodeKind.Each)
		{
			this.variableName = variableName;
			this.list = list;
		}

		public string Variable => variableName;

		public CssValue List => list;
	}

	public class ForBlock : CssNode
	{
		public ForBlock()
			: base(NodeKind.For)
		{ }

		public CssValue Variable { get; set; }

		public CssVariable List { get; set; }
	}

	public class WhileBlock : CssNode
	{
		public WhileBlock()
			: base(NodeKind.While)
		{ }
	}
	*/
}

/*
@each $current-color in $colors-list {
    $i: index($colors-list, $current-color);
    .stuff-#{$i} { 
        color: $current-color;
    }
}
*/