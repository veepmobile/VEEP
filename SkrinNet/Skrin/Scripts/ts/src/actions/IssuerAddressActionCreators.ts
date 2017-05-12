import {REQUEST_ADDRESSES,RECEIVE_ADDRESSES,REQUEST_UPDATE_ADDRESS,RECEIVE_UPDATE_ADDRESS,
    REQUEST_DELETE_ADDRESS,RECEIVE_DELETE_ADDRESS} from '../models/Constants'

import {IAction,IActionId} from '../models/Common'


import {IAddress} from '../models/IAddress'
import IssuerUserAddressesAPI from '../api/IssuerUserAddressesAPI'
import {ModalActionCreators} from '../actions/ModalActionCreators'

export interface IActionReceiveUserAddresses extends IAction{
    addresses:IAddress[]
}

export interface IActionReceiveUserAddress extends IAction{
    address:IAddress
}

export interface IActionUserAddress extends IAction{
    address:IAddress
}


export  class IssuerAddressActionCreators
{
    private static _requestAddresses():IAction{
        return{type:REQUEST_ADDRESSES}
    }

    private static _recieveAddresses(addresses:IAddress[]):IActionReceiveUserAddresses{
        return{type:RECEIVE_ADDRESSES,addresses:addresses}
    }


    static GetAddresses(issuer_id:string){
        return async (dispatch:any)=>{
            try{
                dispatch(this._requestAddresses());
                let addresses = await IssuerUserAddressesAPI.Get(issuer_id);
                dispatch(this._recieveAddresses(addresses));
            }
            catch(error){
                dispatch(ModalActionCreators.showModal("Ошибка загрузки списка контактов: " + (error.message || error)));
            }
        }
    }

    private static _requestUpdateAddress():IAction{
        return{type:REQUEST_UPDATE_ADDRESS}
    }

    private static _recieveUpdateAddress(address:IAddress):IActionReceiveUserAddress{
        return{type:RECEIVE_UPDATE_ADDRESS,address:address}
    }


    static UpdateAddress(address: IAddress){
        return async (dispatch:any)=>{
            try{
                dispatch(this._requestUpdateAddress());
                let addr = await IssuerUserAddressesAPI.Update(address);
                dispatch(this._recieveUpdateAddress(addr))
            }
            catch(error){
                 dispatch(ModalActionCreators.showModal("Ошибка обновления контакта: " + (error.message || error)));
            }
        }
    }    


    private static _requestDeleteAddress():IAction{
        return{type:REQUEST_DELETE_ADDRESS}
    }

    private static _recieveDeleteAddress(address:IAddress):IActionReceiveUserAddress{
        return{type:RECEIVE_DELETE_ADDRESS,address:address}
    }


    static DeleteAddress(id: string){
        return async (dispatch:any)=>{
            try{
                dispatch(this._requestDeleteAddress());
                let address = await IssuerUserAddressesAPI.Delete(id);
                dispatch(this._recieveDeleteAddress(address));
            }
            catch(error){
                 dispatch(ModalActionCreators.showModal("Ошибка удаления контакта: " + (error.message || error)));
            }
        }
    }
}