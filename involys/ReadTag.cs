using Android.App;
using Android.Media;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace involys
{
    public class ReadTag : Fragment
    {
        private MainActivity mContext = null;
        private SoundPool soundPool;
        private int soundPoolBeepOkId, soundPoolBeepNokId;
        private Button readButton;

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            mContext = (MainActivity)Activity;

            readButton = (Button)View.FindViewById(Resource.Id.readButton);
            readButton.Click += delegate { ReadAction(); };

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
            View view = inflater.Inflate(Resource.Layout.read_tag, container, false);

            return view;
        }

        public void ReadAction()
        {
            Log.Debug("NX", "read");

            try
            {
                int userLength = 32;
                string user = mContext.deviceApi.ReadDataFromTag(userLength);
                if (user != null)
                {
                    Toast.MakeText(mContext, user, ToastLength.Short).Show();
                    SoundOK();
                }
                else
                    SoundNOK();
            } catch
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