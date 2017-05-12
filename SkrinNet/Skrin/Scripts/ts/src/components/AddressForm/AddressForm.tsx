import * as React from "react";

import {IAddress,AddressFieldKeys} from '../../models/IAddress'
import {IFunction} from "../../models/Common"

interface IAddressFormProps {
    address:IAddress,
    show_details:boolean,
    updateAddress:IFunction,
    deleteAddress:IFunction,
    issuer_id:string
};

interface IAddressFormState {
    edit_address:IAddress,
    show_details:boolean
};

class AddressForm extends React.Component<IAddressFormProps, IAddressFormState> {


    private is_new:boolean;

    constructor(props:IAddressFormProps){
        super(props);
        this.is_new=this.props.address==null;
        let address=!this.is_new ? {...this.props.address} : this.generateNewAddress();
        this.state={
            edit_address:address,
            show_details:this.props.show_details
        }
         //console.log('constructor. State id:'+this.state.edit_address.id+", props_id:"+ this.props.address.id);
    }
    

    componentWillReceiveProps(nextProps:IAddressFormProps) {
        this.setState({
                show_details:nextProps.show_details
            } as IAddressFormState);
    }

    private generateNewAddress():IAddress{

        return {
            id:null,
            user_id:null,
            issuer_id:this.props.issuer_id,
            name:'',
            phone:'',
            email:'',
            note:'',
            update_date:null,
            extrafields:[]
        };
    }

    private generateSelector(selected_val:number,id:number){
        let options:JSX.Element[]=[];
        for(let i=0;i<AddressFieldKeys.length;i++){
            options.push(<option value={AddressFieldKeys[i].id} key={i}>{AddressFieldKeys[i].name}</option>);
        }
        return(
            <select className="form-control" value={selected_val} onChange={this.changeExtraFieldType.bind(this,id)}>
                {options}
            </select>
        )
    }
    


    private changeFieldValue(field_name:string,event:any){
        let addr=this.state.edit_address as any;
        addr[field_name]=event.target.value;
        this.setState({
            edit_address:addr
        } as IAddressFormState );
    }

    private changeExtraFieldValue(i:number,event:any){
        let addr=this.state.edit_address;
        addr.extrafields[i].value=event.target.value;
        this.setState({
            edit_address:addr
        } as IAddressFormState );
    }

    private changeExtraFieldType(i:number,event:any){
        let addr=this.state.edit_address;
        addr.extrafields[i].key=event.target.value;
        this.setState({
            edit_address:addr
        } as IAddressFormState );
    }


    private clearForm(event:any){
         this.setState({
            edit_address:this.generateNewAddress(),
            show_details:false
        } as IAddressFormState );
        event.preventDefault();
    }

    private resetForm(event:any){
        this.setState({
            edit_address:{...this.props.address},
            show_details:false
        });
        event.preventDefault();
    }

    private submitChange(event:any){  
        if(this.state.edit_address.name!==""){
            
            //удалим дополнительные пустые поля
            let addr=this.state.edit_address;
            for(let i=0;i<addr.extrafields.length;i++){
                if(addr.extrafields[i].value==""){
                    addr.extrafields.splice(i,1);
                }
            }
            this.setState({
                edit_address:addr
            }as IAddressFormState);

            this.props.updateAddress(this.state.edit_address);

            if(this.is_new){       
                this.setState({
                    edit_address:this.generateNewAddress()
                } as IAddressFormState);
            }else{
                this.setState({
                    show_details:false
                } as IAddressFormState);
            }
        }
        event.preventDefault();
    }

    private showDetails(event:any){
        this.setState({
            show_details:true
        } as IAddressFormState);
        event.preventDefault();
    }

    private addExtraField(event:any){
        let addr=this.state.edit_address;
        addr.extrafields.push({key:1,value:""});
        this.setState({
            edit_address:addr
        }as IAddressFormState);
        event.preventDefault();
    }

    private removeExtraField(id:number,event:any){
        let addr=this.state.edit_address;
        addr.extrafields.splice(id,1);
        this.setState({
            edit_address:addr
        }as IAddressFormState)
        event.preventDefault();
    }

    private showHintInfo(){
        if(this.is_new){
            return null;
        }
        let infos=[];
        if(this.props.address.name!=""){
            infos.push("Имя: "+this.props.address.name);
        }
        if(this.props.address.phone!="" && this.props.address.phone!=null){
            infos.push("Телефон: "+this.props.address.phone);
        }
        if(this.props.address.email!="" && this.props.address.email!=null){
            infos.push("Email: "+this.props.address.email);
        }
        for(let i=0;i<this.props.address.extrafields.length;i++){
            let e_info=this.props.address.extrafields[i];
            infos.push(AddressFieldKeys.filter(p=>p.id==e_info.key)[0].name+": "+e_info.value);
        }
         if(this.props.address.note!=""){
            infos.push(this.props.address.note);
        }
        return <em>{infos.map((val,i,arr)=>{
            let divider=i<arr.length-1 ? <br/> : null;
            return (<span>{val}{divider}</span>)
        })}</em>;
    }


    public render(): JSX.Element {
      //  console.log('render. State id:'+this.state.edit_address.id+", props_id:"+ this.props.address.id);

        let form_item:JSX.Element[]=[];

        if(this.state.edit_address.extrafields!=null){
            let fields=this.state.edit_address.extrafields;
             for(var i=0;i<fields.length;i++){
                 form_item.push(
                    <div className="form-group" key={i}>
                        <div className="extrafields_block_item">
                            <div className="extrafields_block_item_left">
                                 {this.generateSelector(fields[i].key,i)}
                                    <input id={"val"+i} type="text" className="form-control" placeholder="Значение" onChange={this.changeExtraFieldValue.bind(this,i)} 
                                        value={fields[i].value}  />
                            </div>
                            <div className="extrafields_block_item_right">
                                <button className="btns red" onClick={this.removeExtraField.bind(this,i)}>-</button>
                            </div>                           
                        </div>                                                  
                    </div> 
                 )
             }
        }
       

        let form = this.state.show_details ? 
            ( <form>
                    <div className="form-group">
                        <label htmlFor="name" className="label_form">Имя</label>
                        <div className="sub_form">
                            <input id="name" type="text" className="form-control" placeholder="Имя" onChange={this.changeFieldValue.bind(this,'name')} 
                            value={this.state.edit_address.name}  />
                        </div>
                    </div>
                    <div className="form-group">
                        <label htmlFor="phone" className="label_form">Телефон</label>
                        <div className="sub_form">
                            <input id="phone" type="text" className="form-control" placeholder="Телефон" onChange={this.changeFieldValue.bind(this,'phone')} 
                            value={this.state.edit_address.phone}  />
                        </div>
                    </div>
                    <div className="form-group">
                        <label htmlFor="email" className="label_form">Email</label>
                        <div className="sub_form">
                            <input id="email" type="text" className="form-control" placeholder="Email" onChange={this.changeFieldValue.bind(this,'email')} 
                            value={this.state.edit_address.email}  />
                        </div>
                    </div>
                    <div className="form-group">
                        <textarea id="note" className="form-control" placeholder="Примечание" onChange={this.changeFieldValue.bind(this,'note')} 
                            value={this.state.edit_address.note}/>
                    </div>
                    <h5>Дополнительная информация</h5>
                    {form_item}
                    <div className="form-group">
                        <button className="btns red" onClick={this.addExtraField.bind(this)}>+</button>
                    </div>
                    <div className="form-group">
                        <button className={this.state.edit_address.name==="" ? "btns darkblue disabled":"btns darkblue"}  onClick={this.submitChange.bind(this)}>Сохранить</button>
                        {
                            this.is_new ? 
                            <button className="btns grey" onClick={this.clearForm.bind(this)}>Отмена</button>
                            :
                            <button className="btns grey" onClick={this.resetForm.bind(this)}>Отмена</button>
                        }
                    </div>
                    
                </form>):null;
        let header=this.state.show_details ? 
            (<h4>
                {this.is_new ? "Новый контакт":this.props.address.name}
            </h4>)
            :
            (<a href="#" onClick={this.showDetails.bind(this)}>
                <span className="title">{this.is_new ? "Добавить контакт":this.props.address.name}
                {this.showHintInfo()}
                </span>
            </a>)

        let delete_block=this.is_new ? null : (
                      <span className="ddel" onClick={this.props.deleteAddress.bind(null,this.props.address.id,this.props.address.name)}>
                        <span className="ddel_1">
                            <b>x</b>
                                Удалить
                            </span>
                        </span>
        );

        return (
                <tr key={this.state.edit_address.id} className="address_item">
                    <td>
                        {delete_block}
                        <div className="icon_block large address_title">
                        {this.is_new && !this.state.show_details ?
                        <span className="icon-plus new ico"></span>
                        :
                        <span className="icon-address-card-o link ico"></span>                            
                        }
                            {header}                            
                        </div>
                        {form}                        
                    </td>
                </tr>               
            );
    }
}

export default AddressForm;
