import Navbar from "./components/Navbar";
import { useState } from "react";

import TextInput from "./components/TextInput";
import WordsBox from "./components/WordsBox";
import './style/App.css'

function App() {
  const [words, setWords] = useState<string[]>([]);
  const [language, setLanguage] = useState<string>('it');
  return (
    <div className="translator-root">
      <Navbar />
      <div className="big-container">
        <TextInput onTextClick={setWords} setLanguage={setLanguage} language={language}/>
        <WordsBox words={words} language={language}/>
      </div>



    </div>
  )
}

export default App
