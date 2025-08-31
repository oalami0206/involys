namespace involys
{
    using Android.App;
    using Android.Media;
    using Android.OS;
    using Android.Util;
    using Android.Views;
    using Android.Widget;
    using System.Collections.Generic;
    using System.Threading;

    [Activity(Label = "Inventory")]
    public class Inventory : Fragment
    {
        private MainActivity mContext = null;
        private SoundPool soundPool;
        private int soundPoolBeepOkId, soundPoolBeepNokId;
        private Button inventoryButton;

        UIHand handler;
        private bool IsScanning = false;
        private List<string> uidList;

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            mContext = (MainActivity)Activity;

            inventoryButton = (Button)View.FindViewById(Resource.Id.inventoryButton);
            inventoryButton.Click += delegate { InventoryAction(); };

            try
            {
                soundPool = new SoundPool(10, Stream.Music, 0);
                soundPoolBeepOkId = soundPool.Load(mContext, Resource.Raw.beep_ok, 1);
                soundPoolBeepNokId = soundPool.Load(mContext, Resource.Raw.beep_nok, 1);
            }
            catch
            {
                // Handle missing sound resources gracefully
                soundPool = null;
            }

            uidList = new List<string>();

            handler = new UIHand(this);
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.inventory, container, false);
            return view;
        }

        public void InventoryAction()
        {
            Log.Debug("NX", "Inventory");

            if (!IsScanning)
            {
                try
                {
                    mContext.deviceApi.StartInventory();

                    Toast.MakeText(mContext, Resource.String.start, ToastLength.Short).Show();
                    inventoryButton.SetText(Resource.String.stop);

                    ReadInventoryTags();
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
                    mContext.deviceApi.StopInventory();
                    IsScanning = false;

                    Toast.MakeText(mContext, Resource.String.stop, ToastLength.Short).Show();
                    inventoryButton.SetText(Resource.String.start);
                }
                catch
                {
                    Toast.MakeText(mContext, "failed", ToastLength.Short).Show();
                    SoundNOK();
                }
            }

        }

        private void ReadInventoryTags()
        {
            IsScanning = true;

            Thread th = new Thread(
                    new ThreadStart(
                            delegate
                            {
                                while (IsScanning)
                                {
                                    try
                                    {
                                        NxMobSdk.TagInfo tagInfo = mContext.deviceApi.ReadTagFromBuffer();
                                        if (tagInfo != null && tagInfo.GetUid() != "")
                                        {
                                            string uid = tagInfo.GetUid();

                                            if (uidList.FindIndex(s => s==uid) == -1)
                                            {
                                                uidList.Add(uid);
                                                Message msg = handler.ObtainMessage();
                                                msg.Obj = tagInfo.GetUid();
                                                handler.SendMessage(msg);
                                                SoundOK();
                                            }
                                        }

                                    }
                                    catch { }
                                }
                            }
                        )
                );

            th.Start();
        }

        private void SoundOK() { 
            if (soundPool != null) 
                soundPool.Play(soundPoolBeepOkId, 1, 1, 0, 0, 1); 
        }
        private void SoundNOK() { 
            if (soundPool != null) 
                soundPool.Play(soundPoolBeepNokId, 1, 1, 0, 0, 1); 
        }

        private class UIHand : Handler
        {
            readonly Inventory inventoryFragment;

            public UIHand(Inventory _inventoryFragment)
            {
                inventoryFragment = _inventoryFragment;
            }

            public override void HandleMessage(Message msg)
            {
                try
                {
                    string uid = msg.Obj + "";
                    Log.Debug("NX_UID", uid);
                    Toast.MakeText(inventoryFragment.mContext, uid, ToastLength.Short).Show();
                }
                catch { }

                base.HandleMessage(msg);
            }
        }
    }

}
