

export const getAPIUrl = () => {
    console.log('if dev: ', import.meta.env.DEV)
    const api = import.meta.env.DEV ? "http://localhost:5139/api/" : "api/";
    console.log('api: ', api)
    return api
}

export const childApiUrl = {
    location: {
        url : "locations/",
        add : "add",
        update: "update",
        delete: "delete",
        upload: "upload",
        primarykey: "locationId"
    }
}

const apiUrl = "http://localhost:5139/api/";
export default apiUrl;
