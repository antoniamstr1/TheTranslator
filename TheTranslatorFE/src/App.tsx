import Navbar from "./components/Navbar";
import { useState, useEffect } from "react";
import axios from "axios";
import TextInput from "./components/TextInput";
import WordsBox from "./components/WordsBox";
import './style/App.css'

function App() {

  async function getOrCreateUserCode(): Promise<string> {
    // gledam da li ima user u localStorage, ako ne kreiram ga (workspace zapravo)
    const storedCode = localStorage.getItem("user_code");

    if (storedCode) return storedCode;


    const response = await axios.post(`${import.meta.env.VITE_API_BASE_URL}/user`);

    const code = response.data[0].code.toString();
    localStorage.setItem("user_code", code);

    return code;
  }

  const [userCode, setUserCode] = useState<string>();

  useEffect(() => {
    async function init() {
      const code = await getOrCreateUserCode();
      setUserCode(code);
    }
    init();
  }, []);

  const [words, setWords] = useState<string[]>([]);
  const [language, setLanguage] = useState<string>('it');
  const [hasText, setHasText] = useState(false);
  const [text, setText] = useState("");


  return (
    <div className="translator-root">
      <Navbar hasText={hasText} languageFrom={language} text={text} setText={setText} userCode={userCode} setUserCode={setUserCode} setLanguage={setLanguage} setWords={setWords}/>
      <div className="big-container">
        <TextInput onTextClick={setWords} setLanguage={setLanguage} language={language} setHasText={setHasText} text={text} setText={setText} />
        <WordsBox words={words} language={language} />
      </div>



    </div>
  )
}

export default App
