import * as React from "react";

import {IAddress} from '../../models/IAddress'
import {IDispatchGet,IFunction} from '../../models/Common'

import AddressForm from '../AddressForm/AddressForm'


interface IDispatchSaveAddress{
    (address: IAddress,can_use_cloud:boolean):void;
}

interface IIssuerUserAddressProps {
    addresses:IAddress[],
    can_use_cloud:boolean,
    getAddresses: IDispatchGet,
    saveAddress:IDispatchSaveAddress,
    deleteAddress:IFunction,
    issuer_id:string
};


interface IIssuerUserAddressState {
};

class IssuerUserAddress extends React.Component<IIssuerUserAddressProps, IIssuerUserAddressState> {

    private is_load_data=false;

    componentDidMount() {
        this.props.getAddresses(this.props.issuer_id); 
        this.is_load_data=true;               
    }
 
    
    updateAddress(address:IAddress){
        this.props.saveAddress(address,this.props.can_use_cloud);
    }


    public render(): JSX.Element {

       

        let addresses=this.props.addresses.map((address)=>{
            return(
                <AddressForm address={address} show_details={false} key={address.id} issuer_id={this.props.issuer_id} 
                deleteAddress={this.props.deleteAddress.bind(this)}  updateAddress={this.updateAddress.bind(this)} />
            )
        })

        return (
            
            <div className="link_block" id="lb_0">  
                <h3 className="cloud_switcher_header"><span className="icon-users"></span>Список контактов
                    <span className="icon-angle-down cloud_switcher_ar"></span>
                </h3>
                <table className="extra_table" style={{border:0}} cellPadding="0" cellSpacing="0">
                        <tbody>                            
                            {addresses}
    <AddressForm key={0} address={null} show_details={this.is_load_data && this.props.addresses.length==0}  updateAddress={this.updateAddress.bind(this)} 
                           issuer_id={this.props.issuer_id}  deleteAddress={this.props.deleteAddress.bind(null)}  />
                        </tbody> 
                    </table> 
            </div>
        );
    }
}

export default IssuerUserAddress;
