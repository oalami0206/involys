namespace involys
{
    using Android.App;
    using Android.Media;
    using Android.OS;
    using Android.Util;
    using Android.Views;
    using Android.Widget;
    using System.Threading;

    [Activity(Label = "Search")]
    public class Search : Fragment
    {
        private MainActivity mContext = null;
        private SoundPool soundPool;
        private int soundPoolBeepOkId, soundPoolBeepNokId;
        private Button searchButton;

        UIHand handler;
        private bool IsSearching = false;
        readonly string uidToFind = "E280110520007ACD2E5C0A69";

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            mContext = (MainActivity)Activity;

            searchButton = (Button)View.FindViewById(Resource.Id.searchButton);
            searchButton.Click += delegate { SearchAction(); };

            soundPool = new SoundPool(10, Stream.Music, 0);
            soundPoolBeepOkId = soundPool.Load(mContext, Resource.Drawable.beep_ok, 1);
            soundPoolBeepNokId = soundPool.Load(mContext, Resource.Drawable.beep_nok, 1);

            handler = new UIHand(mContext, searchButton);
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.search, container, false);
            return view;
        }

        public void SearchAction()
        {
            Log.Debug("NX", "Search");

            if (!IsSearching)
            {
                try
                {
                    SearchTag();

                    IsSearching = true;
                    Toast.MakeText(mContext, Resource.String.start, ToastLength.Short).Show();
                    searchButton.SetText(Resource.String.stop);
                }
                catch
                {
                    Toast.MakeText(mContext, "failed", ToastLength.Short).Show();
                    SoundNOK();
                }
            }
            else
            {
                try
                {
                    mContext.deviceApi.StopSearch();
                    IsSearching = false;

                    Toast.MakeText(mContext, Resource.String.stop, ToastLength.Short).Show();
                    searchButton.SetText(Resource.String.search);
                }
                catch
                {
                    Toast.MakeText(mContext, "failed", ToastLength.Short).Show();
                    SoundNOK();
                }
            }

        }

        private void SearchTag()
        {
            Thread th = new Thread(
                    new ThreadStart(
                            delegate
                            {
                                try
                                {
                                    bool result = mContext.deviceApi.Search(uidToFind);

                                    if (result) SoundOK();
                                    else SoundNOK();

                                    IsSearching = false;

                                    Message msg = handler.ObtainMessage();
                                    msg.Obj = result;
                                    handler.SendMessage(msg);
                                }
                                catch
                                {
                                    SoundNOK();
                                }

                                
                            }
                        )
                );

            th.Start();
        }

        private void SoundOK() { soundPool.Play(soundPoolBeepOkId, 1, 1, 0, 0, 1); }
        private void SoundNOK() { soundPool.Play(soundPoolBeepNokId, 1, 1, 0, 0, 1); }

        private class UIHand : Handler
        {
            readonly MainActivity mHandlerContext;
            private readonly Button searchButton;

            public UIHand(MainActivity _mContext, Button _searchButton)
            {
                mHandlerContext = _mContext;
                searchButton = _searchButton;
            }

            public override void HandleMessage(Message msg)
            {
                try
                {
                    searchButton.SetText(Resource.String.search);
                    Toast.MakeText(mHandlerContext, Resource.String.stop, ToastLength.Short).Show();
                }
                catch { }

                base.HandleMessage(msg);
            }
        }
    }

}
