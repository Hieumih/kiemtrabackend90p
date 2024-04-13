

export const getAPIUrl = () => {
    console.log('if dev: ', import.meta.env.DEV)
    return import.meta.env.DEV ? "http://localhost:5139/api/" : "api/";
}

const apiUrl = "http://localhost:5139/api/";
export default apiUrl;
