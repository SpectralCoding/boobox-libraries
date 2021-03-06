﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BooBox {

	public enum RepeatMode {
		Off,
		One,
		All
	}

	public enum ShuffleMode {
		Off,
		On
	}

	public enum ConnectionMode {
		LibraryRequest,
		SongRequest,
		OnlineTest,
		PlaylistListRequest,
		PlaylistRequest,
		FirstConnect
	}

	public enum ConnectionStatus {
		Pending,
		Connected
	}

}
