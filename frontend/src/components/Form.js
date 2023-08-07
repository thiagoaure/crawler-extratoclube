import { mask, unMask } from "node-masker"
import { useState } from "react"
import Benefit from "./Benefit"
import api from "../services/apiService"
import "../styles/Form.css"

function Form() {

    const [cpf, setCPF] = useState("")
    const [data, setData] = useState("")

    const searchBenefit = async (e) => {
        e.preventDefault()
    
        try {
            if (cpf === '') console.log("nenhum cpf informado")
    
            const response = await api.get(`user/${mask(cpf, "999.999.999-99")}`)

            if (response.data.data.existRegistrationNumber) {
                setData(response.data.data.registrationNumber)
            } else {
                setData(response.data.message)
            }
        } catch(err) {
            console.log(err.message)
        }
    }
    return (
      <div>
        <div>
            <form>
            <div className="content-container">
            <input 
                placeholder="Digite o número do cpf"
                type="text" 
                onChange={({ target }) => {
                    setCPF(unMask(target.value))
                }}
                value={mask(cpf, "999.999.999-99")}
            />
            <button type="submit" onClick={searchBenefit}>Buscar</button>
            </div>
            </form>
            <Benefit data={data}/>
        </div>
      </div>
    );
  }
  
  export default Form;