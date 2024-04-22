import {url_link_icon, getLangData} from "@/common/common";

const lang = getLangData();
console.log('lang: ', lang);

const dashboardUrl = new url_link_icon("/dashboard", lang.dashboard, "bi bi-table");
const userUrl = new url_link_icon("/account", lang.account, "fa-regular fa-address-card");
const customerUrl = new url_link_icon("/customer", lang.customer, "bi bi-person-circle");
const locationUrl = new url_link_icon("/location", lang.location, "bi bi-geo-alt");
const productUrl = new url_link_icon("/product", lang.product, "bi bi-box");
const orderUrl = new url_link_icon("/prder", lang.order, "fa-solid fa-basket-shopping");
const supportUrl = new url_link_icon("/support", lang.support, "bi bi-question-circle");
const interactionUrl = new url_link_icon("/interaction", lang.interaction, "bi bi-chat-left-text");
const scheduleUrl = new url_link_icon("/schedule", lang.schedule, "fa-solid fa-calendar-days");
const projectUrl = new url_link_icon("/project", lang.project, "fa-solid fa-diagram-project");
const phoneCallUrl = new url_link_icon("/phone-call", lang.phoneCall, "fa-solid fa-phone");

// const locationManagerUrl = new url_link_icon("/manager", "Manager", "bi bi-geo-alt");
// locationUrl.child.push(locationManagerUrl);


export const url_paths = [
    dashboardUrl,
    // userUrl,
    customerUrl,
    // productUrl,
    // orderUrl,
    locationUrl,
    // supportUrl,
    // interactionUrl,
    scheduleUrl,
    // projectUrl,
    phoneCallUrl
]