using AndroidX.AppCompat.App;
using RSS_Reader.Fragments;
using static System.Net.Mime.MediaTypeNames;

namespace RSS_Reader.Activities;

[Activity(Label = "@string/app_name", MainLauncher = true)]
public class RssfeedActivity : AppCompatActivity, MyListFragment.OnItemSelectedListener
{
    SelectionStateFragment stateFragment;
    private bool _shouldAnimate = true;

    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        SetContentView(Resource.Layout.activity_rssfeed);

        stateFragment = FragmentManager.FindFragmentByTag<SelectionStateFragment>("headless");

        if (stateFragment == null) 
        {
            stateFragment = new SelectionStateFragment();
            FragmentManager.BeginTransaction().Add(stateFragment, "headless").Commit();
        }

        if (_towPaneMode()) {
            // restore state
            if (stateFragment.LastSelection.Length > 0) 
            {
                onRssItemSelected(stateFragment.LastSelection);
            }
            // otherwise all is good, we use the fragments defined in the layout
            return;
        }

        // if savedInstanceState is null we do some cleanup
        if (savedInstanceState != null) {
            // cleanup any existing fragments in case we are in detailed mode
            FragmentManager.ExecutePendingTransactions();
            Fragment fragmentById = FragmentManager.FindFragmentById<Fragment>(Resource.Id.fragment_container);
            if (fragmentById != null)
            {
                FragmentManager.BeginTransaction()
                    .Remove(fragmentById)
                    .Commit();
            }
        }
        MyListFragment myListFragment = new MyListFragment();
        FragmentManager.BeginTransaction()
            .Replace(Resource.Id.fragment_container, myListFragment)
            .Commit();
        _shouldAnimate = true;
        if (stateFragment.LastSelection.Length > 0)
        {
            _shouldAnimate = false;
            onRssItemSelected(stateFragment.LastSelection);
            return;
        }
    }

    public void onRssItemSelected(string text)
    {
        stateFragment.LastSelection = text;
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
            if (_shouldAnimate)
            {
                transaction.SetCustomAnimations(Resource.Animator.slide_up, Resource.Animator.slide_down);
            }
            transaction.Replace(Resource.Id.fragment_container, newFragment);
            transaction.AddToBackStack(null);
            transaction.Commit();
        }
    }

    private bool _towPaneMode() => Resources.GetBoolean(Resource.Boolean.twoPaneMode);
}