using System;

namespace Dotnet.Enums.API
{
	[Flags]
	public enum EnumActions : byte
	{
		Unsettled = 0,
		Add = 1,
		Update = 2,
		Get = 3,
		Remove = 4,
		Delete = 5,
	}
}