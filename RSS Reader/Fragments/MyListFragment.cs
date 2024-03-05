using Android.Content;
using Android.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Android.Widget.AdapterView;

namespace RSS_Reader.Fragments
{
    public class MyListFragment : Fragment
    {
        OnItemSelectedListener listener;

        public override View? OnCreateView(LayoutInflater? inflater, ViewGroup? container, Bundle? savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.fragment_rsslist_overview, container, false);
            Button button = view.FindViewById<Button>(Resource.Id.updateButton);
            button.Click += (s, e) => {
                UpdateDetail("testing");
            };
            return view;
        }

        public override void OnAttach(Context? context)
        {
            base.OnAttach(context);
            if (context is OnItemSelectedListener) 
            {
                listener = (OnItemSelectedListener)context;
            }
            else
            {
                throw new System.InvalidCastException(context.ToString()
                        + " must implement YourNamespace.OnItemSelectedListener");
            }
        }

        public void UpdateDetail(String uri)
        {
            long currentTimeMillis = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            // create fake data
            string newTime = currentTimeMillis.ToString() + uri;
            // send data to activity
            listener.onRssItemSelected(newTime);
        }

        public interface OnItemSelectedListener
        {   void onRssItemSelected(String text);
        }
    }
}
