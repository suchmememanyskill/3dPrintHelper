using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiLinker.Generic;
using ApiLinker.Interfaces;
using ApiLinker.PrusaPrintables.Models;
using Newtonsoft.Json;

namespace ApiLinker.PrusaPrintables
{
    public class PrintablesApi : IApi
    {
        public string ApiName() => "Prusa Printables";

        public Colour ApiColour() => new Colour(250, 104, 49);

        private Dictionary<string, string> sortTypes = new Dictionary<string, string>()
        {
            {"Newest", "-first_publish"},
            {"Random", "random"},
            {"Most downloaded last 7 days", "-download_count_7_days"},
            {"Most downloaded last 7 days only new", "-download_count_7_days"},
            {"Most downloaded last 30 days", "-download_count_30_days"},
            {"Most downloaded last 30 days only new", "-download_count_30_days"},
            {"Most downloaded all time", "-download_count"},
            {"Most liked last 7 days", "-likes_count_7_days"},
            {"Most liked last 7 days only new", "-likes_count_7_days"},
            {"Most liked last 30 days", "-likes_count_30_days"},
            {"Most liked last 30 days only new", "-likes_count_30_days"},
            {"Most liked all time", "-likes_count"},
            {"Most makes last 7 days", "-makes_count_7_days"},
            {"Most makes last 30 days", "-makes_count_30_days"},
            {"Most makes all time", "-makes_count"},
            {"Most viewed last 7 days", "-display_count_7_days"},
            {"Most viewed last 30 days", "-display_count_30_days"},
            {"Most viewed all time", "-display_count"},
            {"Top rated last 7 days", "-rating_avg_7_days"},
            {"Top rated last 30 days", "-rating_avg_30_days"},
            {"Top rated all time", "-rating_avg"},
        };

        public List<string> SortTypes() => sortTypes.Keys.ToList();
        
        private string GetTemplate => new("{\"operationName\":\"PrintList\",\"variables\":{\"limit\":{{LIMIT}},\"categoryId\":null,\"publishedDateLimitDays\":{{PUBLISHDATE}},\"hasMake\":false,\"competitionAwarded\":false,\"featured\":false,\"likedByMe\":false,\"collectedByMe\":false,\"madeByMe\":false,\"ordering\":\"{{DISPLAY}}\",\"cursor\":{{CURSOR}}},\"query\":\"query PrintList($limit: Int!, $cursor: String, $categoryId: ID, $materialIds: [Int], $userId: ID, $printerIds: [Int], $licenses: [ID], $ordering: String, $hasModel: Boolean, $filesType: [FilterPrintFilesTypeEnum], $includeUserGcodes: Boolean, $nozzleDiameters: [Float], $weight: IntervalObject, $printDuration: IntervalObject, $publishedDateLimitDays: Int, $featured: Boolean, $featuredNow: Boolean, $usedMaterial: IntervalObject, $hasMake: Boolean, $competitionAwarded: Boolean, $onlyFollowing: Boolean, $collectedByMe: Boolean, $madeByMe: Boolean, $likedByMe: Boolean) {\\n  morePrints(\\n    limit: $limit\\n    cursor: $cursor\\n    categoryId: $categoryId\\n    materialIds: $materialIds\\n    printerIds: $printerIds\\n    licenses: $licenses\\n    userId: $userId\\n    ordering: $ordering\\n    hasModel: $hasModel\\n    filesType: $filesType\\n    nozzleDiameters: $nozzleDiameters\\n    includeUserGcodes: $includeUserGcodes\\n    weight: $weight\\n    printDuration: $printDuration\\n    publishedDateLimitDays: $publishedDateLimitDays\\n    featured: $featured\\n    featuredNow: $featuredNow\\n    usedMaterial: $usedMaterial\\n    hasMake: $hasMake\\n    onlyFollowing: $onlyFollowing\\n    competitionAwarded: $competitionAwarded\\n    collectedByMe: $collectedByMe\\n    madeByMe: $madeByMe\\n    liked: $likedByMe\\n  ) {\\n    cursor\\n    items {\\n      ...PrintListFragment\\n      printer {\\n        id\\n        __typename\\n      }\\n      user {\\n        rating\\n        __typename\\n      }\\n      __typename\\n    }\\n    __typename\\n  }\\n}\\n\\nfragment PrintListFragment on PrintType {\\n  id\\n  name\\n  slug\\n  ratingAvg\\n  ratingCount\\n  likesCount\\n  liked\\n  datePublished\\n  dateFeatured\\n  firstPublish\\n  downloadCount\\n  displayCount\\n  inMyCollections\\n  foundInUserGcodes\\n  userGcodeCount\\n  userGcodesCount\\n  materials {\\n    id\\n    __typename\\n  }\\n  category {\\n    id\\n    path {\\n      id\\n      name\\n      __typename\\n    }\\n    __typename\\n  }\\n  modified\\n  images {\\n    ...ImageSimpleFragment\\n    __typename\\n  }\\n  filesType\\n  hasModel\\n  user {\\n    ...AvatarUserFragment\\n    __typename\\n  }\\n  ...LatestCompetitionResult\\n  __typename\\n}\\n\\nfragment AvatarUserFragment on UserType {\\n  id\\n  publicUsername\\n  avatarFilePath\\n  slug\\n  badgesProfileLevel {\\n    profileLevel\\n    __typename\\n  }\\n  __typename\\n}\\n\\nfragment LatestCompetitionResult on PrintType {\\n  latestCompetitionResult {\\n    placement\\n    competitionId\\n    __typename\\n  }\\n  __typename\\n}\\n\\nfragment ImageSimpleFragment on PrintImageType {\\n  id\\n  filePath\\n  rotation\\n  __typename\\n}\\n\"}");
        private string ModelTemplate => new("{\"operationName\":\"PrintProfile\",\"variables\":{\"id\":\"{{ID}}\"},\"query\":\"query PrintProfile($id: ID!) {\\n  print(id: $id) {\\n    ...PrintDetailFragment\\n    __typename\\n  }\\n}\\n\\nfragment PrintDetailFragment on PrintType {\\n  id\\n  slug\\n  name\\n  eduProject {\\n    id\\n    subject {\\n      id\\n      name\\n      slug\\n      __typename\\n    }\\n    timeDifficulty\\n    audienceAge\\n    complexity\\n    equipment {\\n      id\\n      name\\n      __typename\\n    }\\n    suitablePrinters {\\n      id\\n      name\\n      __typename\\n    }\\n    organisation\\n    authors\\n    targetGroupFocus\\n    knowledgeAndSkills\\n    objectives\\n    equipmentDescription\\n    timeSchedule\\n    workflow\\n    approved\\n    datePublishRequested\\n    __typename\\n  }\\n  user {\\n    ...AvatarUserFragment\\n    email\\n    donationLinks {\\n      id\\n      title\\n      url\\n      __typename\\n    }\\n    printsCount\\n    __typename\\n  }\\n  ratingAvg\\n  myRating\\n  ratingCount\\n  description\\n  category {\\n    id\\n    path {\\n      id\\n      name\\n      description\\n      __typename\\n    }\\n    __typename\\n  }\\n  modified\\n  firstPublish\\n  datePublished\\n  dateCreatedThingiverse\\n  hasModel\\n  summary\\n  shareCount\\n  likesCount\\n  makesCount\\n  liked\\n  printDuration\\n  numPieces\\n  weight\\n  nozzleDiameters\\n  usedMaterial\\n  layerHeights\\n  materials {\\n    name\\n    __typename\\n  }\\n  dateFeatured\\n  downloadCount\\n  displayCount\\n  filesCount\\n  collectionsCount\\n  pdfFilePath\\n  commentCount\\n  userGcodeCount\\n  userGcodesCount\\n  remixCount\\n  canBeRated\\n  inMyCollections\\n  printer {\\n    id\\n    name\\n    __typename\\n  }\\n  images {\\n    ...ImageSimpleFragment\\n    __typename\\n  }\\n  tags {\\n    name\\n    id\\n    __typename\\n  }\\n  thingiverseLink\\n  filesType\\n  foundInUserGcodes\\n  license {\\n    id\\n    disallowRemixing\\n    __typename\\n  }\\n  remixParents {\\n    ...remixParentDetail\\n    __typename\\n  }\\n  gcodes {\\n    id\\n    name\\n    filePath\\n    fileSize\\n    filePreviewPath\\n    __typename\\n  }\\n  stls {\\n    id\\n    name\\n    filePath\\n    fileSize\\n    filePreviewPath\\n    __typename\\n  }\\n  slas {\\n    id\\n    name\\n    filePath\\n    fileSize\\n    filePreviewPath\\n    __typename\\n  }\\n  ...LatestCompetitionResult\\n  competitions {\\n    id\\n    name\\n    slug\\n    description\\n    __typename\\n  }\\n  competitionResults {\\n    placement\\n    competition {\\n      id\\n      name\\n      slug\\n      printsCount\\n      openedFrom\\n      openedTo\\n      __typename\\n    }\\n    __typename\\n  }\\n  __typename\\n}\\n\\nfragment LatestCompetitionResult on PrintType {\\n  latestCompetitionResult {\\n    placement\\n    competitionId\\n    __typename\\n  }\\n  __typename\\n}\\n\\nfragment AvatarUserFragment on UserType {\\n  id\\n  publicUsername\\n  avatarFilePath\\n  slug\\n  badgesProfileLevel {\\n    profileLevel\\n    __typename\\n  }\\n  __typename\\n}\\n\\nfragment remixParentDetail on PrintRemixType {\\n  id\\n  parentPrintId\\n  parentPrintName\\n  parentPrintAuthor {\\n    id\\n    slug\\n    publicUsername\\n    __typename\\n  }\\n  parentPrint {\\n    id\\n    name\\n    slug\\n    datePublished\\n    images {\\n      ...ImageSimpleFragment\\n      __typename\\n    }\\n    license {\\n      id\\n      name\\n      disallowRemixing\\n      __typename\\n    }\\n    __typename\\n  }\\n  __typename\\n}\\n\\nfragment ImageSimpleFragment on PrintImageType {\\n  id\\n  filePath\\n  rotation\\n  __typename\\n}\\n\"}");
        private string SearchTemplate => new("{\"operationName\":\"SearchPrints\",\"variables\":{\"query\":\"{{QUERY}}\",\"ordering\":null,\"limit\":{{LIMIT}},\"offset\":{{OFFSET}}},\"query\":\"query SearchPrints($query: String, $limit: Int, $offset: Int, $ordering: SearchChoicesEnum) {\\n  result: searchPrints(\\n    query: $query\\n    printType: print\\n    limit: $limit\\n    offset: $offset\\n    ordering: $ordering\\n  ) {\\n    items {\\n      ...SearchPrintsFragment\\n      __typename\\n    }\\n    __typename\\n  }\\n}\\n\\nfragment SearchPrintsFragment on SearchPrintType {\\n  id\\n  score\\n  explanation\\n  mainImage\\n  ratingAvg\\n  ratingCount\\n  filesType\\n  hasModel\\n  name\\n  ...LatestCompetitionResultSearch\\n  downloadCount\\n  displayCount\\n  dateFeatured\\n  datePublished\\n  slug\\n  liked\\n  likesCount\\n  user {\\n    ...AvatarSearchFragment\\n    __typename\\n  }\\n  __typename\\n}\\n\\nfragment AvatarSearchFragment on SearchUserType {\\n  id\\n  slug\\n  publicUsername\\n  avatarFilePath\\n  badgesProfileLevel {\\n    profileLevel\\n    __typename\\n  }\\n  __typename\\n}\\n\\nfragment LatestCompetitionResultSearch on SearchPrintType {\\n  latestCompetitionResult {\\n    placement\\n    competitionId\\n    __typename\\n  }\\n  __typename\\n}\\n\"}");
        
        private string lastSortType = "";
        private int lastAmount = 0;
        private string lastCursor = "null";
        private List<PrintablesPreviewPost> previewPostCache = new();
        
        public async Task<List<IPreviewPost>> GetPosts(string sortType, int amount, int skip)
        {
            if (lastSortType == sortType && lastAmount == amount)
            {
                if (previewPostCache.Count >= amount + skip)
                {
                    return previewPostCache.Skip(skip).Take(amount).Select(x => (IPreviewPost)x).ToList();
                }
            }
            else
            {
                ClearCache();
            }
            
            string dateLimit = "null";
            string ordering = sortTypes[sortType];

            if (sortType.Contains("only new"))
            {
                if (sortType.Contains("7 days"))
                    dateLimit = "7";
                else if (sortType.Contains("30 days"))
                    dateLimit = "30";
            }

            string template = GetTemplate;

            string json = template
                .Replace("{{LIMIT}}", amount.ToString())
                .Replace("{{PUBLISHDATE}}", dateLimit)
                .Replace("{{CURSOR}}", lastCursor)
                .Replace("{{DISPLAY}}", ordering);

            string response = await Request.PostStringAsync(new Uri("https://www.printables.com/graphql/"), json);

            PrintList prints = JsonConvert.DeserializeObject<PrintList>(response);
            lastSortType = sortType;
            lastCursor = $"\"{prints.Data.MorePrints.Cursor}\"";
            lastAmount = amount;
            previewPostCache.AddRange(prints.Data.MorePrints.Items.Select(x => new PrintablesPreviewPost(this, x)));
            return prints.Data.MorePrints.Items.Select(x => (IPreviewPost) new PrintablesPreviewPost(this, x)).ToList();
        }

        public async Task<List<IPreviewPost>> GetPostsBySearch(string search, int amount, int skip)
        {
            string template = SearchTemplate;
            string json = template
                .Replace("{{QUERY}}", search)
                .Replace("{{LIMIT}}", amount.ToString())
                .Replace("{{OFFSET}}", skip.ToString());
            
            string response = await Request.PostStringAsync(new Uri("https://www.printables.com/graphql/"), json);
            
            PrintList prints = JsonConvert.DeserializeObject<PrintList>(response);
            return prints.Data.SearchResult.Items.Select(x => (IPreviewPost) new PrintablesSearchPreviewPost(this, x)).ToList();
        }

        public async Task<PrintablesPost> GetPost(PrintablesPreviewPost previewPost)
        {
            string template = ModelTemplate;
            string json = template.Replace("{{ID}}", previewPost.Id());
            
            string response = await Request.PostStringAsync(new Uri("https://www.printables.com/graphql/"), json);

            PrintModelData model = JsonConvert.DeserializeObject<PrintModelData>(response);
            return new PrintablesPost(this, model!.Data.Print, previewPost);
        }

        private void ClearCache()
        {
            lastSortType = "";
            lastCursor = "null";
            lastAmount = 0;
            previewPostCache.Clear();
        }
    }
}