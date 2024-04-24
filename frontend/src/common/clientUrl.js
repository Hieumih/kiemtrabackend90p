import {url_link_icon, getLangData} from "@/common/common";

const lang = getLangData();
console.log('lang: ', lang);

const dashboardUrl = new url_link_icon("/dashboard", lang.dashboard, "bi bi-table");
const hangHoaUrl = new url_link_icon("/hang-hoa", "hàng hóa", "bi bi-box");
const nhomHangHoaUrl = new url_link_icon("/nhom-hang-hoa", "nhóm hàng hóa", "bi bi-box");

// const locationManagerUrl = new url_link_icon("/manager", "Manager", "bi bi-geo-alt");
// locationUrl.child.push(locationManagerUrl);


export const url_paths = [
    dashboardUrl,
    hangHoaUrl,
    nhomHangHoaUrl

]