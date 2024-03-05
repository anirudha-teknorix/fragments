using RSS_Reader.Fragments;

namespace RSS_Reader;

[Activity(Label = "@string/app_name", MainLauncher = true)]
public class RssfeedActivity : Activity, MyListFragment.OnItemSelectedListener
{
    
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        SetContentView(Resource.Layout.activity_rssfeed);
        // Create your application here
    }

    public void onRssItemSelected(string text)
    {
        DetailFragment fragment = FragmentManager.FindFragmentById<DetailFragment>(Resource.Id.detailFragment);
        fragment?.SetText(text);
    }
}