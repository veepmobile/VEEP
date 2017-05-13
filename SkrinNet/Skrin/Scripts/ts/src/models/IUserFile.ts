import {IUserData} from './IUserData'

export  interface IUserFile extends IUserData
{
    file_name:string;
    file_size:number;
    total_file_limit:number;
}