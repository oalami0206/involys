using Android.App;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;
using static involys.MainActivity;

namespace involys
{
    public class ScanBarcode1D : Fragment
    {
        private MainActivity mContext = null;

        private Button scan1dButton;

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            mContext = (MainActivity)Activity;

            scan1dButton = (Button)View.FindViewById(Resource.Id.scan1dButton);
            scan1dButton.Click += delegate { Scan1dAction(); };
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.scan_barcode_1d, container, false);
            return view;
        }

        public void Scan1dAction()
        {
            Log.Debug("NX", "Scan 1D");

            MainCallback mainCallback = new MainCallback(mContext);
            mContext.deviceApi.ScanBarcode1D(mContext, mainCallback);
        }
    }
}