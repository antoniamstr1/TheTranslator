import "../style/WordsBox.css";
import { useState, useEffect } from 'react';
import axios from "axios";


interface WordsProps {
    words: string[];
    language: string;
}

interface ConjugationForm {
    type: string;
    value: string;
}

interface ConjugationPersons {
    [person: string]: ConjugationForm;
}

interface Conjugation {
    [tense: string]: ConjugationPersons;
}

interface Analysis {
    type: string;
    forms: {
        word: string;
        tags: string[];
    };
    definitions: string[];
}

interface WordDetails {
    analysis: Analysis;
    conjugation?: Conjugation;
}

type WordError = { error: string };


export default function WordsBox({ words, language }: WordsProps) {

    const [openButtons, setOpenButtons] = useState(() => Array(words.length).fill(false));

    const [detailsData, setDetailsData] = useState<(WordDetails | WordError | null)[]>(
        Array(words.length).fill(null)
    );
    const API_BASE_URL = `${import.meta.env.VITE_API_BASE_URL}/${language}/`;

    const handleClick = async (i: number) => {

        setOpenButtons(prev => {
            const newArr = [...prev];
            newArr[i] = !newArr[i];
            return newArr;
        });
        if (!detailsData[i]) {
            try {
                const response = await axios.get(`${API_BASE_URL}${words[i]}`);
                
                setDetailsData(prev => {
                    const newArr = [...prev];
                    newArr[i] = response.data;
                    return newArr;
                });
            }
            catch (error: unknown) {
                if (axios.isAxiosError(error)) {
                    /* ŠTO AKO VRATI 400 -> ne može riješiti infinitiv */
                    if (error.response?.status === 400) {
                        setDetailsData(prev => {
                            const newArr = [...prev];
                            newArr[i] = { error: "Infinitive not found" };
                            return newArr;
                        });
                        /* 404 znači da nije riječ u ciljanom jeziku */
                    } else if (error.response?.status === 404) {
                        setDetailsData(prev => {
                            const newArr = [...prev];
                            newArr[i] = { error: "Word not found" };
                            return newArr;
                        });
                    } else {
                        console.error("Unhandled axios error:", error);
                    }
                } else {
                    console.error("Non-Axios error:", error);
                }
            }
        }
    };

    useEffect(() => {
        setOpenButtons(Array(words.length).fill(false));
        setDetailsData(Array(words.length).fill(null));
    }, [words]);

    function isWordDetails(data: WordDetails | WordError | null | undefined): data is WordDetails {
        return (
            data !== null &&
            data !== undefined &&
            "analysis" in data
        );
    }

    return (
        <div className="words-container">
            <div className="words">
                {words.map((word, index) => (
                    openButtons[index] ? (
                        <div key={index} className="expanded-row">
                            <div className="word-container">
                                <div className="details-container">
                                    <button
                                        className="word"
                                        onClick={() => handleClick(index)}
                                    >
                                        {word}
                                    </button>

                                    <div className="details-box">
                                        {isWordDetails(detailsData[index]) ? (
                                            <>
                                                <div style={{ color: "gray" }}>
                                                    {detailsData[index].analysis.type}
                                                </div>

                                                <div>
                                                    {detailsData[index].analysis.definitions?.join(", ")}
                                                </div>

                                                {detailsData[index].conjugation && (
                                                    <div className="conjugations-box">
                                                        {Object.entries(detailsData[index].conjugation).map(
                                                            ([tenseName, persons], tIdx) => (
                                                                <div key={tIdx} className="conjugations-container">
                                                                    <div className="tense">{tenseName}</div>
                                                                    <div className="verbs-container">
                                                                        {Object.entries(persons).map(
                                                                            ([person, form], fIdx) => (
                                                                                <div key={fIdx} className="verb-row">
                                                                                    <div className="verb-face">{person}</div>
                                                                                    <div
                                                                                        className="verb-conjugated"
                                                                                        style={{
                                                                                            color:
                                                                                                form.type !== "regular"
                                                                                                    ? "#c43838fe"
                                                                                                    : "black",
                                                                                        }}
                                                                                    >
                                                                                        {form.value}
                                                                                    </div>
                                                                                </div>
                                                                            )
                                                                        )}
                                                                    </div>
                                                                </div>
                                                            )
                                                        )}
                                                    </div>
                                                )}
                                            </>
                                        ) : detailsData[index] ? (
                                            <div style={{ color: "gray" }}>{detailsData[index].error}</div>
                                        ) : null}
                                    </div>
                                </div>





                            </div>

                        </div>
                    ) : (
                        <button
                            key={index}
                            className="word"
                            onClick={() => handleClick(index)}
                        >
                            {word}
                        </button>
                    )
                ))}
            </div>
        </div>
    )
}