using Android.OS;
using Android.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSS_Reader.Fragments
{
    public class DetailFragment : Fragment
    {
        public static string EXTRA_TEXT ="text";

        public override View? OnCreateView(LayoutInflater? inflater, ViewGroup? container, Bundle? savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.fragment_rssitem_detail, container, false);
            return view;
        }

        public override void OnActivityCreated(Bundle? savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            Bundle bundle = Arguments;
            if (bundle != null) {
                string text = bundle.GetString(EXTRA_TEXT);
                SetText(text);
            }
        }

        public void SetText(string text) {
            TextView textView = View.FindViewById<TextView>(Resource.Id.detailsText);
            textView.Text = text;
        }
    }
}
