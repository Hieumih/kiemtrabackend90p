import {url_link_icon} from "@/common"

const dashboardUrl = new url_link_icon("/dashboard", "Dashboard", "bi bi-table");
const customerUrl = new url_link_icon("/customer", "Customer", "bi bi-person-circle", []);
const customerstatisticUrl = new url_link_icon("/statistic", "Statistic", "bi bi-person-circle");
customerUrl.child.push(customerstatisticUrl);
const locationUrl = new url_link_icon("/location", "Location", "bi bi-geo-alt", []);
const locationManagerUrl = new url_link_icon("/manager", "Manager", "bi bi-geo-alt");
locationUrl.child.push(locationManagerUrl);


export const url_paths = [
    dashboardUrl,
    customerUrl,
    locationUrl
]