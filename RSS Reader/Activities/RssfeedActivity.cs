using RSS_Reader.Fragments;

namespace RSS_Reader.Activities;

[Activity(Label = "@string/app_name", MainLauncher = true)]
public class RssfeedActivity : Activity, MyListFragment.OnItemSelectedListener
{

    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        SetContentView(Resource.Layout.activity_rssfeed);

        if (_towPaneMode()) {
            return;
        }

        if (savedInstanceState != null) {
            FragmentManager.ExecutePendingTransactions();
            Fragment fragmentContainer = FragmentManager.FindFragmentById<Fragment>(Resource.Id.fragment_container);
            if (fragmentContainer != null)
            {
                FragmentManager.BeginTransaction()
                    .Remove(fragmentContainer)
                    .Commit();
            }
        }
        MyListFragment myListFragment = new MyListFragment();
        FragmentManager.BeginTransaction()
            .Replace(Resource.Id.fragment_container, myListFragment)
            .Commit();
    }

    public void onRssItemSelected(string text)
    {
        if (_towPaneMode())
        {
            DetailFragment fragment = FragmentManager.FindFragmentById<DetailFragment>(Resource.Id.detailFragment);
            fragment?.SetText(text);
        } else
        {
            // replace the fragment
            // Create fragment and give it an argument for the selected article
            DetailFragment newFragment = new DetailFragment();
            Bundle args = new Bundle();
            args.PutString(DetailFragment.EXTRA_TEXT, text);
            newFragment.Arguments = args;
            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            // Replace whatever is in the fragment_container view with this fragment,
            // and add the transaction to the back stack so the user can navigate back
            transaction.Replace(Resource.Id.fragment_container, newFragment);
            transaction.AddToBackStack(null);
            transaction.Commit();
        }
    }

    private bool _towPaneMode() => Resources.GetBoolean(Resource.Boolean.twoPaneMode);
}