/* eslint-disable @typescript-eslint/no-explicit-any */
import axios from 'axios';
import { ENVIRONMENT } from '../config/env';

export class HttpError<T> extends Error
{
    code: number;
    data: T;

    constructor(name: string, message: string, code: number, data: T) {
        super(name);
        this.name = name;
        this.message = message;
        this.code = code;
        this.data = data;
    }
}

export const axiosInstance = axios.create({
  baseURL: `${ baseUrl()}/api/v1`,
});

async function thenCatch(promise: Promise<any>) {
    return promise
      .then((res) => res.data)
      .catch((err) => {
          throw new HttpError(err.name, err.message, err.code, err.response?.data);
      });
}

export async function get(url: string, headers = { }) {
  return thenCatch(axiosInstance.get(url, updateAuthToken(headers)));
}

export async function put(url: string, { arg }: { arg: any }, headers = { }) {
    return await thenCatch(axiosInstance.put(url, arg, updateAuthToken(headers)));
}

export async function post(url: string, { arg }: { arg: any }, headers = { }) {
   console.log('post', arg);
    return await thenCatch(axiosInstance.post(url, arg, updateAuthToken(headers)));
}

function updateAuthToken(headers: any) {
  return {
    headers: {
    ...headers,
    },
  };
}

function baseUrl()
{
    return ENVIRONMENT.DEV ? 'http://localhost:5116' : 'https://crypto-predict-api.azurewebsites.net';
}

export const getEvents = () => thenCatch(axiosInstance.get('/events'));
