import mitt from 'mitt';

export const EventBus = mitt();

export class url_link_icon {
    constructor(url, title, icon, child = null) {
        this.url = url;
        this.title = title;
        /**@type {url_link_icon[]} */
        this.child = child;
        this.icon = icon;
    }
}