import mitt from 'mitt';
import moment from 'moment-timezone';

export const EventBus = mitt();

export const addNotification = (message, toast_type) => {
    EventBus.emit('addNotification', { message, toast_type });
}

export class url_link_icon {
    constructor(url, title, icon, child = []) {
        this.url = url;
        this.title = title;
        /**@type {url_link_icon[]} */
        this.child = child;
        this.icon = icon;
    }
}

export const getLangClient = async (lang) => {
    // Set default language to the client language
    await fetch('lang/' + lang + '.json')
        .then(response => response.json())
        .then(data => {
            localStorage.setItem('langData', JSON.stringify(data));
            console.log('data: ', data); 1
        })
        .catch(async error => {
            console.error(error);
            localStorage.setItem('lang', 'en-US');
            await getLang('en-US');
        })
}

export const getLangData = () => {
    const lang = JSON.parse(localStorage.getItem('langData'));
    return lang;
}

export const getTimeZones = () => {
    return moment.tz.names();
}

export const formatTimeZone = (timeZone) => {
    const offset = moment.tz(timeZone).utcOffset();
    const sign = offset >= 0 ? '+' : '-';
    const hours = Math.floor(Math.abs(offset) / 60);
    const minutes = Math.abs(offset) % 60;
    const formattedOffset = `${sign}${hours.toString().padStart(2, '0')}:${minutes.toString().padStart(2, '0')}`;

    // Extracting city name from time zone string
    const cityIndex = timeZone.lastIndexOf('/') + 1;
    const city = timeZone.substring(cityIndex).replace(/_/g, ' ');

    return `(UTC${formattedOffset}) ${city}/${timeZone}`;
}

export const formatTimeZoneJson = (timeZone) => {
    return { text: formatTimeZone(timeZone), value: timeZone };
}


export const formatTimeZoneJsonList = () => {
    const timeZones = moment.tz.names();
    const compactedTimeZones = [];
    timeZones.map(timeZone => {
        const offset = moment.tz(timeZone).format('Z');
        const text = `(UTC${offset}) ${timeZone}`;
        const value = timeZone;
        compactedTimeZones.push({ text, value, offset });
    });
    // sort by offset then value
    compactedTimeZones.sort((a, b) => {
        if (a.offset < b.offset) return -1;
        if (a.offset > b.offset) return 1;
        if (a.value < b.value) return -1;
        if (a.value > b.value) return 1;
        return 0;
    });
    return compactedTimeZones;
}