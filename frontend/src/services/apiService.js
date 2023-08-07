import axios from "axios"

const api = axios.create({
    baseURL: 'http://localhost:5102/api/v1/'
})

export default api;