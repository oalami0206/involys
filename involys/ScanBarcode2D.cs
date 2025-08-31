using Android.App;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;
using static involys.MainActivity;

namespace involys
{
    public class ScanBarcode2D : Fragment
    {
        private MainActivity mContext = null;

        private Button scan2dButton;

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            mContext = (MainActivity)Activity;

            scan2dButton = (Button)View.FindViewById(Resource.Id.scan2dButton);
            scan2dButton.Click += delegate { Scan2dAction(); };
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.scan_barcode_2d, container, false);
            return view;
        }

        public void Scan2dAction()
        {
            Log.Debug("NX", "Scan 2D");

            MainCallback mainCallback = new MainCallback(mContext);

            mContext.deviceApi.ScanBarcode2D(mContext, mainCallback);
        }
    }
}