using System;
using Newtonsoft.Json.Linq;

namespace MiHomeLib.Devices
{
    public class WirelessDualWallSwitch : MiHomeDevice
    {
        private const string LeftChannel = "channel_0";
        private const string RightChannel = "channel_1";

        public event Action<EventArgs> OnRightClick;
        public event Action<EventArgs> OnLeftClick;

        public event Action<EventArgs> OnRightDoubleClick;
        public event Action<EventArgs> OnLeftDoubleClick;

        public event Action<EventArgs> OnRightLongClick;
        public event Action<EventArgs> OnLeftLongClick;

        public string StatusLeft { get; private set; }
        public string StatusRight { get; private set; }

        public override void ParseData(string command)
        {
            var jObject = JObject.Parse(command);

            if (jObject[LeftChannel] != null)
            {
                StatusLeft = jObject[LeftChannel].Value<string>();
                if (StatusLeft == "click")
                {
                    OnLeftClick?.Invoke(new EventArgs());
                }
                else if (StatusLeft == "double_click")
                {
                    OnLeftDoubleClick?.Invoke(new EventArgs());
                }
                else if (StatusLeft == "long_click")
                {
                    OnLeftLongClick?.Invoke(new EventArgs());
                }

            }

            if (jObject[RightChannel] != null)
            {
                StatusRight = jObject[RightChannel].Value<string>();
                if (StatusRight == "click")
                {
                    OnRightClick?.Invoke(new EventArgs());
                }
                else if (StatusRight == "double_click")
                {
                    OnRightDoubleClick?.Invoke(new EventArgs());
                }
                else if (StatusRight == "long_click")
                {
                    OnRightLongClick?.Invoke(new EventArgs());
                }
            }

        }

        public WirelessDualWallSwitch(string sid) : base(sid, "remote.b286acn01")
        {

        }

        public override string ToString()
        {
            return $"Status Left: {StatusLeft}, Right: {StatusRight}";
        }
    }
}