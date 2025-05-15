import axios from 'axios';

const apiClient = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL,
  withCredentials: true,
  timeout: 10000,
});

apiClient.interceptors.response.use(
  (resp) => resp,
  (err) => {
    console.error('API Error:', err);
    return Promise.reject(err);
  }
);

export default {
  get: apiClient.get,
  post: apiClient.post,
};
