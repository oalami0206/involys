using Android.App;
using Android.Media;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace involys
{
    public class SingleInventory : Fragment
    {
        private MainActivity mContext = null;
        private SoundPool soundPool;
        private int soundPoolBeepOkId, soundPoolBeepNokId;
        private Button singleInventoryButton;

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            mContext = (MainActivity)Activity;

            singleInventoryButton = (Button)View.FindViewById(Resource.Id.singleInventoryButton);
            singleInventoryButton.Click += delegate { SingleInventoryAction(); };

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
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.single_inventory, container, false);

            return view;
        }

        public void SingleInventoryAction()
        {
            Log.Debug("NX", "single inventory");

            try
            {
                NxMobSdk.TagInfo tagInfo = mContext.deviceApi.SingleRead();
                if (tagInfo != null)
                {
                    Toast.MakeText(mContext, tagInfo.GetUid(), ToastLength.Short).Show();
                    SoundOK();
                }
                else
                    SoundNOK();
            }
            catch
            {
                SoundNOK();
            }
        }

        private void SoundOK() { 
            if (soundPool != null) 
                soundPool.Play(soundPoolBeepOkId, 1, 1, 0, 0, 1); 
        }
        private void SoundNOK() { 
            if (soundPool != null) 
                soundPool.Play(soundPoolBeepNokId, 1, 1, 0, 0, 1); 
        }
    }
}