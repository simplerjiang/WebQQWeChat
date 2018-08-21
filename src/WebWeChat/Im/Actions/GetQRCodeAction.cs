﻿using System.ComponentModel;
using System.Threading.Tasks;
using HttpAction.Core;
using HttpAction.Event;
using HttpAction;
using WebWeChat.Im.Core;

namespace WebWeChat.Im.Actions
{
    /// <summary>
    /// 获取二维码
    /// </summary>
    [Description("获取二维码")]
    public class GetQRCodeAction : WebWeChatAction
    {
        public GetQRCodeAction(IWeChatContext context, ActionEventListener listener = null) : base(context, listener)
        {
        }

        protected override HttpReq BuildRequest()
        {
            var req =  new HttpReq(HttpMethodType.Post, string.Format(ApiUrls.GetQRCode, Session.Uuid));
            req.AddData("t", "webwx");
            req.AddData("_", (Session.Seq++).ToString());
            req.ResultType = HttpResultType.Byte;
            return req;
        }

        protected override ValueTask<ActionEvent> HandleResponse(HttpRes responseItem)
        {
            // return NotifyOkEventAsync(Image.FromStream(responseItem.ResponseStream));
            return NotifyOkEventAsync(ImageSharp.Image.Load(responseItem.ResponseBytes));
        }
    }
}
