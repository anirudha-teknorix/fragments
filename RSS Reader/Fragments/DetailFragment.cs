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
        // private TextView textView;

        public override View? OnCreateView(LayoutInflater? inflater, ViewGroup? container, Bundle? savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.fragment_rssitem_detail, container, false);
            return view;
        }

        public void SetText(string text) {
            TextView textView = View.FindViewById<TextView>(Resource.Id.detailsText);
            textView.Text = text;
        }
    }
}
