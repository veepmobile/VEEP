import {IUserData} from './IUserData'

export interface IAddress extends IUserData
{
    name:string;
    phone:string;
    email:string;
    note:string;
    extrafields:IAddressField[]
}


export interface IAddressField
{
    key:number;
    value:string;
}


export interface IAddressFieldKeys
{
    id:number;
    name:string;
}

export const AddressFieldKeys:IAddressFieldKeys[]=
[
    {
        id:1,
        name:"Должность"
    },
    {
        id:2,
        name:"Веб-сайт"
    },
    {
        id:3,
        name:"Адрес"
    },
    {
        id:4,
        name:"Домашний телефон"
    },
    {
        id:5,
        name:"Мобильный телефон"
    }
    
]
    
