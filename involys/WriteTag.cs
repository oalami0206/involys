using Android.App;
using Android.Media;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace involys
{
    public class WriteTag : Fragment
    {
        private MainActivity mContext = null;
        private SoundPool soundPool;
        private int soundPoolBeepOkId, soundPoolBeepNokId;
        private Button writeButton;

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            mContext = (MainActivity)Activity;

            writeButton = (Button)View.FindViewById(Resource.Id.writeButton);
            writeButton.Click += delegate { WriteAction(); };

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

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.write_tag, container, false);
            return view;
        }

        public void WriteAction()
        {
            Log.Debug("NX", "Write");

            try
            {
                int userLength = 32;
                string user = new string('1', userLength*4);
                bool result = mContext.deviceApi.WriteDataToTag(user, userLength);
                if (result)
                {
                    Toast.MakeText(mContext, "OK", ToastLength.Short).Show();
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

        private void SoundOK() { soundPool.Play(soundPoolBeepOkId, 1, 1, 0, 0, 1); }
        private void SoundNOK() { soundPool.Play(soundPoolBeepNokId, 1, 1, 0, 0, 1); }
    }
}