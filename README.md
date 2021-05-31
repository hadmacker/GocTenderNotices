# Government of Canada Tender Notices Analysis

Personal project to track changes to the [Government of Canada's Tender Notices open data set](https://open.canada.ca/data/en/dataset/ffd38960-1853-4c19-ba26-e50bea2cb2d5)

# About the Data Set

* [Government of Canada's Tender Notices](https://open.canada.ca/data/en/dataset/ffd38960-1853-4c19-ba26-e50bea2cb2d5)
* Data Set License: [Open Government License - Canada](https://open.canada.ca/en/open-government-licence-canada)

## Files

* [Active (tpsgc-pwgsc_ao-t_a.csv)](https://buyandsell.gc.ca/procurement-data/csv/tender/active)
  * Approx. Size: 7MB
* [Awarded (tpsgc-pwgsc_aa-a.csv)](https://buyandsell.gc.ca/procurement-data/csv/award/all)
  * Approx. Size: 262MB and growing
* [Expired (tpsgc-pwgsc_ao-t_a_b-g.csv)](https://buyandsell.gc.ca/procurement-data/csv/tender/expired)
  * Approx. Size: 203MB and growing
* [New Today (tpsgc-pwgsc_ao-t_n.csv)](https://buyandsell.gc.ca/procurement-data/csv/tender/new-today)

## Feeds & Data Sources

* [Buyandsell.gc.ca/procurement-data](https://buyandsell.gc.ca/procurement-data/)
* [Follow Opportunities](https://buyandsell.gc.ca/procurement-data/tenders/follow-opportunities)
  * Supports Email notifications & RSS/Atom Feeds

Alternative feeds to reduce bandwidth requirements of downloading and re-processing the every-growing Awarded and Expired datasets listed below:

* Awarded Feed [RSS](https://buyandsell.gc.ca/procurement-data/feed?dds_facet_date_published=NOW/DAY-7DAYS%20TO%20NOW/DAY%2B86399999MILLISECONDS&sm_facet_procurement_data=data_data_tender_award&ss_language=en&rss_atom_title=%7B%22sm_facet_procurement_data%22%3A%5B%22data_data_tender_award%22%5D%2C%22dds_facet_date_published%22%3A%5B%22dds_facet_date_published_7day%22%5D%7D) [ATOM](https://buyandsell.gc.ca/procurement-data/feed/atom?dds_facet_date_published=NOW/DAY-7DAYS%20TO%20NOW/DAY%2B86399999MILLISECONDS&sm_facet_procurement_data=data_data_tender_award&ss_language=en&rss_atom_title=%7B%22sm_facet_procurement_data%22%3A%5B%22data_data_tender_award%22%5D%2C%22dds_facet_date_published%22%3A%5B%22dds_facet_date_published_7day%22%5D%7D)
  * Awarded tender notices published in the last 7 days
* Expired Feed [RSS](https://buyandsell.gc.ca/procurement-data/feed?dds_facet_date_published=NOW/DAY-7DAYS%20TO%20NOW/DAY%2B86399999MILLISECONDS&ss_publishing_status=SDS-SS-006&sm_facet_procurement_data=%28tender_notice%20AND%20data_data_tender_notice%29&ss_language=en&rss_atom_title=%7B%22sm_facet_procurement_data%22%3A%5B%22tender_notice%22%2C%22data_data_tender_notice%22%5D%2C%22ss_publishing_status%22%3A%5B%22SDS-SS-006%22%5D%2C%22dds_facet_date_published%22%3A%5B%22dds_facet_date_published_7day%22%5D%7D) [ATOM](https://buyandsell.gc.ca/procurement-data/feed/atom?dds_facet_date_published=NOW/DAY-7DAYS%20TO%20NOW/DAY%2B86399999MILLISECONDS&ss_publishing_status=SDS-SS-006&sm_facet_procurement_data=%28tender_notice%20AND%20data_data_tender_notice%29&ss_language=en&rss_atom_title=%7B%22sm_facet_procurement_data%22%3A%5B%22tender_notice%22%2C%22data_data_tender_notice%22%5D%2C%22ss_publishing_status%22%3A%5B%22SDS-SS-006%22%5D%2C%22dds_facet_date_published%22%3A%5B%22dds_facet_date_published_7day%22%5D%7D)
  * Expired tender notices published in the last 7 days
* Amended Feed [RSS](https://buyandsell.gc.ca/procurement-data/feed?dds_facet_date_amended=NOW/DAY-7DAYS%20TO%20NOW/DAY%2B86399999MILLISECONDS&sm_facet_procurement_data=%28tender_notice%20AND%20data_data_tender_notice%29&ss_language=en&rss_atom_title=%7B%22sm_facet_procurement_data%22%3A%5B%22tender_notice%22%2C%22data_data_tender_notice%22%5D%2C%22dds_facet_date_amended%22%3A%5B%22dds_facet_date_amended_7day%22%5D%7D) [ATOM](https://buyandsell.gc.ca/procurement-data/feed/atom?dds_facet_date_amended=NOW/DAY-7DAYS%20TO%20NOW/DAY%2B86399999MILLISECONDS&sm_facet_procurement_data=%28tender_notice%20AND%20data_data_tender_notice%29&ss_language=en&rss_atom_title=%7B%22sm_facet_procurement_data%22%3A%5B%22tender_notice%22%2C%22data_data_tender_notice%22%5D%2C%22dds_facet_date_amended%22%3A%5B%22dds_facet_date_amended_7day%22%5D%7D)
  * Tender notices amended in the last 7 days
* Active Feed [RSS](https://buyandsell.gc.ca/procurement-data/feed?dds_facet_date_published=NOW/DAY-7DAYS%20TO%20NOW/DAY%2B86399999MILLISECONDS&ss_publishing_status=SDS-SS-005&sm_facet_procurement_data=%28tender_notice%20AND%20data_data_tender_notice%29&ss_language=en&rss_atom_title=%7B%22sm_facet_procurement_data%22%3A%5B%22tender_notice%22%2C%22data_data_tender_notice%22%5D%2C%22dds_facet_date_published%22%3A%5B%22dds_facet_date_published_7day%22%5D%2C%22ss_publishing_status%22%3A%5B%22SDS-SS-005%22%5D%7D) [ATOM](https://buyandsell.gc.ca/procurement-data/feed/atom?dds_facet_date_published=NOW/DAY-7DAYS%20TO%20NOW/DAY%2B86399999MILLISECONDS&ss_publishing_status=SDS-SS-005&sm_facet_procurement_data=%28tender_notice%20AND%20data_data_tender_notice%29&ss_language=en&rss_atom_title=%7B%22sm_facet_procurement_data%22%3A%5B%22tender_notice%22%2C%22data_data_tender_notice%22%5D%2C%22dds_facet_date_published%22%3A%5B%22dds_facet_date_published_7day%22%5D%2C%22ss_publishing_status%22%3A%5B%22SDS-SS-005%22%5D%7D)
  * Active tender notices published in the last 7 days.

# TODO

* ARM provisioning script to build environment
* Evaluate RSS or ATOM feed parsing
* Build Atom/Rss feed job loader
* Define entry data structure & transitions
* Define background processor
* Build Website skeleton
* Define Storage & Bandwidth limitations & Alerting
* .NET Core 3.1 or serverless
* DevOps all the things. Yaml pipeline, etc.